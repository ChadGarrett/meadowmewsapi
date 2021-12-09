using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API.Interfaces;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Controllers
{
    [Authorize]
    public class ElectricityController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ElectricityController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Electricity
        [HttpGet]
        public async Task<IEnumerable<ElectricityPurchaseDto>> GetStatements()
        {
            return await _unitOfWork.electricityRepository.GetPurchases();
        }

        // GET: api/Electricity/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityPurchase>> GetStatement(int id)
        {
            var electricityStatement = await _unitOfWork.electricityRepository.GetPurchase(id);

            if (electricityStatement == null) 
            {
                return NotFound();
            }

            return electricityStatement;
        }

        // POST: api/Electricity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElectricityPurchase>> AddPurchase(AddElectricityDto electricityPurchase)
        {
            var purchase = _mapper.Map<ElectricityPurchase>(electricityPurchase);
            
            await _unitOfWork.electricityRepository.AddPurchase(purchase);

            if (await _unitOfWork.Complete()) 
            {
                return CreatedAtAction(nameof(GetStatement), new { id = purchase.Id }, electricityPurchase);
            }

            return BadRequest("Failed to add a new Electricity Purchase");
        }

        // DELETE: api/Electricity/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatement(int id)
        {
            var purchase = await _unitOfWork.electricityRepository.GetPurchase(id);

            if (purchase == null)
            {
                return NotFound();
            }
            
            _unitOfWork.electricityRepository.DeletePurchase(purchase);
            
            if (await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Unable to delete Electricity Purchase");
        }
    }
}