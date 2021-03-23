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
    public class WaterStatementsController : ControllerBase
    {
        private readonly WaterStatementContext _context;

        public WaterStatementsController(WaterStatementContext context)
        {
            _context = context;
        }

        // GET: api/WaterStatements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaterStatementDTO>>> GetWaterStatement()
        {
            return await _context.WaterStatement
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/WaterStatements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WaterStatementDTO>> GetWaterStatement(long id)
        {
            var waterStatement = await _context.WaterStatement.FindAsync(id);

            if (waterStatement == null)
            {
                return NotFound();
            }

            return ItemToDTO(waterStatement);
        }

        // PUT: api/WaterStatements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaterStatement(long id, WaterStatementDTO waterStatementDto)
        {
            if (id != waterStatementDto.Id)
            {
                return BadRequest();
            }

            var waterStatement = await _context.WaterStatement.FindAsync(id);

            if (waterStatement == null)
            {
                return NotFound();
            }

            waterStatement.Date = waterStatement.Date;
            waterStatement.Amount = waterStatementDto.Amount;
            waterStatement.Paid = waterStatement.Paid;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!WaterStatementExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/WaterStatements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WaterStatementDTO>> PostWaterStatement(WaterStatementDTO waterStatementDto)
        {
            var waterStatement = new WaterStatement
            {
                HouseholdId = waterStatementDto.HouseholdId,
                Date = waterStatementDto.Date,
                Amount = waterStatementDto.Amount,
                Paid = waterStatementDto.Paid,
            };
            
            _context.WaterStatement.Add(waterStatement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetWaterStatement), 
                new { id = waterStatement.Id }, 
                ItemToDTO(waterStatement));
        }

        // DELETE: api/WaterStatements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaterStatement(long id)
        {
            var waterStatement = await _context.WaterStatement.FindAsync(id);
            if (waterStatement == null)
            {
                return NotFound();
            }

            _context.WaterStatement.Remove(waterStatement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helpers
        private bool WaterStatementExists(long id)
        {
            return _context.WaterStatement.Any(e => e.Id == id);
        }

        // Mappers
        private static WaterStatementDTO ItemToDTO(WaterStatement waterStatement) => new WaterStatementDTO
        {
            Id = waterStatement.Id,
            HouseholdId = waterStatement.HouseholdId,
            Date = waterStatement.Date,
            Amount = waterStatement.Amount,
            Paid = waterStatement.Paid,
        };
    }
}
