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
    public class LevyStatementsController : ControllerBase
    {
        private readonly LevyStatementContext _context;

        public LevyStatementsController(LevyStatementContext context)
        {
            _context = context;
        }

        // GET: api/LevyStatements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LevyStatement>>> GetLevyStatements()
        {
            return await _context.LevyStatements.ToListAsync();
        }

        // GET: api/LevyStatements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LevyStatement>> GetLevyStatement(long id)
        {
            var levyStatement = await _context.LevyStatements.FindAsync(id);

            if (levyStatement == null)
            {
                return NotFound();
            }

            return levyStatement;
        }

        // PUT: api/LevyStatements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLevyStatement(long id, LevyStatement levyStatement)
        {
            if (id != levyStatement.Id)
            {
                return BadRequest();
            }

            _context.Entry(levyStatement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevyStatementExists(id))
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

        // POST: api/LevyStatements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LevyStatement>> PostLevyStatement(LevyStatement levyStatement)
        {
            _context.LevyStatements.Add(levyStatement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLevyStatement), new { id = levyStatement.Id }, levyStatement);
        }

        // DELETE: api/LevyStatements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevyStatement(long id)
        {
            var levyStatement = await _context.LevyStatements.FindAsync(id);
            if (levyStatement == null)
            {
                return NotFound();
            }

            _context.LevyStatements.Remove(levyStatement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LevyStatementExists(long id)
        {
            return _context.LevyStatements.Any(e => e.Id == id);
        }
    }
}
