using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeadowMewsApi.Models;

namespace MeadowMewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseholdsController : ControllerBase
    {
        private readonly HouseholdContext _context;
        private readonly UserContext _userContext;

        public HouseholdsController(HouseholdContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: api/Households
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseholdDTO>>> GetHousehold()
        {
            return await _context.Household
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Households/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HouseholdDTO>> GetHousehold(long id)
        {
            var household = await _context.Household.FindAsync(id);

            if (household == null)
            {
                return NotFound();
            }

            return ItemToDTO(household);
        }

        // PUT: api/Households/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHousehold(long id, HouseholdDTO householdDto)
        {
            if (id != householdDto.Id)
            {
                return BadRequest();
            }

            var household = await _context.Household.FindAsync(id);

            if (household == null)
            {
                return NotFound();
            }

            household.UserId = householdDto.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!HouseholdExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Households
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HouseholdDTO>> PostHousehold(HouseholdDTO householdDto)
        {
            var household = new Household
            {
                UserId = householdDto.UserId,
            };
            
            _context.Household.Add(household);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetHousehold), 
                new { id = household.Id }, 
                ItemToDTO(household));
        }

        // DELETE: api/Households/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHousehold(long id)
        {
            var household = await _context.Household.FindAsync(id);
            if (household == null)
            {
                return NotFound();
            }

            _context.Household.Remove(household);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helpers
        private bool HouseholdExists(long id)
        {
            return _context.Household.Any(e => e.Id == id);
        }

        // Mappers

        private static HouseholdDTO ItemToDTO(Household household)
        {
            return new HouseholdDTO
            {
                Id = household.Id,
                UserId = household.UserId,
            };
        }
    }
}
