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
        public async Task<ActionResult<IEnumerable<WaterStatement>>> GetWaterStatement()
        {
            return await _context.WaterStatement.ToListAsync();
        }

        // GET: api/WaterStatements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WaterStatement>> GetWaterStatement(long id)
        {
            var waterStatement = await _context.WaterStatement.FindAsync(id);

            if (waterStatement == null)
            {
                return NotFound();
            }

            return waterStatement;
        }

        // PUT: api/WaterStatements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaterStatement(long id, WaterStatement waterStatement)
        {
            if (id != waterStatement.Id)
            {
                return BadRequest();
            }

            _context.Entry(waterStatement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaterStatementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WaterStatements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WaterStatement>> PostWaterStatement(WaterStatement waterStatement)
        {
            _context.WaterStatement.Add(waterStatement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWaterStatement", new { id = waterStatement.Id }, waterStatement);
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

        private bool WaterStatementExists(long id)
        {
            return _context.WaterStatement.Any(e => e.Id == id);
        }
    }
}
