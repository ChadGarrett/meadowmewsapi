using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (registerDto.Email.Count() == 0)
            {
                return BadRequest("Precondition failed: Email is required");
            }
            
            if (await UserExists(registerDto.Email))
            {
                return BadRequest("Email already in use");
            }

            var user = _mapper.Map<AppUser>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) 
            {
                return BadRequest(result.Errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, AppRoleType.Member.ToString());

            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(p => p.Photo)
                .SingleOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return new UserDto 
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }

        /////////////
        // Helpers //
        /////////////

        private async Task<bool> UserExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }
    }
}