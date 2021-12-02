using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using API.Interfaces;
using API.Entities;

namespace MeadowMewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityStatementsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ElectricityStatementsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ElectricityStatements/GetStatements
        [HttpGet]
        public async Task<IEnumerable<ElectricityPurchase>> GetStatements()
        {
            return await _unitOfWork.electricityRepository.GetPurchases();
        }

        // GET: api/ElectricityStatements/GetStatements/1
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

        // POST: api/ElectricityStatements/AddPurchase
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

        // DELETE: api/ElectricityStatements/DeleteStatement/5
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
