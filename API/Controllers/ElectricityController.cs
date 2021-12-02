using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API.Interfaces;
using API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MeadowMewsApi.Controllers
{
    [Authorize]
    public class ElectricityController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ElectricityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Electricity
        [HttpGet]
        public async Task<IEnumerable<ElectricityPurchase>> GetStatements()
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
        public async Task<ActionResult<ElectricityPurchase>> AddPurchase(ElectricityPurchase electricityPurchase)
        {
            await _unitOfWork.electricityRepository.AddPurchase(electricityPurchase);

            if (await _unitOfWork.Complete()) 
            {
                return CreatedAtAction(nameof(GetStatement), new { id = electricityPurchase.Id }, electricityPurchase);
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