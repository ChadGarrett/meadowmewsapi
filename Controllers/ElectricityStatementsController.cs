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
    public class ElectricityStatementsController : ControllerBase
    {
        private readonly ElectricityStatementContext _context;

        public ElectricityStatementsController(ElectricityStatementContext context)
        {
            _context = context;
        }

        // GET: api/ElectricityStatements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityStatement>>> GetElectricityStatement()
        {
            return await _context.ElectricityStatement.ToListAsync();
        }

        // GET: api/ElectricityStatements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityStatement>> GetElectricityStatement(long id)
        {
            var electricityStatement = await _context.ElectricityStatement.FindAsync(id);

            if (electricityStatement == null) 
            {
                return NotFound();
            }

            return electricityStatement;
        }

        // PUT: api/ElectricityStatements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricityStatement(long id, ElectricityStatement electricityStatement)
        {
            if (id != electricityStatement.Id)
            {
                return BadRequest();
            }

            _context.Entry(electricityStatement).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityStatementExists(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ElectricityStatements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElectricityStatement>> PostElectricityStatement(ElectricityStatement electricityStatement)
        {
            await _context.ElectricityStatement.AddAsync(electricityStatement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElectricityStatement), new { id = electricityStatement.Id }, electricityStatement);
        }

        // DELETE: api/ElectricityStatements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricityStatement(long id)
        {
            var electricityStatement = await _context.ElectricityStatement.FindAsync(id);
            
            if (electricityStatement == null) 
            {
                return NotFound();
            }

            _context.ElectricityStatement.Remove(electricityStatement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectricityStatementExists(long id)
        {
            return _context.ElectricityStatement.Any(e => e.Id == id);
        }
    }
}
