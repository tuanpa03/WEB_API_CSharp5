using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_BanHang.Models;

namespace Web_BanHang.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategroiesAPIController : ControllerBase
    {
        private readonly BanHangContext _context;

        public CategroiesAPIController(BanHangContext context)
        {
            _context = context;
        }

        // GET: api/Categroies
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Categroies>>> GetCategroies()
        {
          if (_context.Categroies == null)
          {
              return NotFound();
          }
            return await _context.Categroies.ToListAsync();
        }

        // GET: api/Categroies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categroies>> GetCategroies(int id)
        {
          if (_context.Categroies == null)
          {
              return NotFound();
          }
            var categroies = await _context.Categroies.FindAsync(id);

            if (categroies == null)
            {
                return NotFound();
            }

            return categroies;
        }

        // PUT: api/Categroies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-categories/{id}")]
        public async Task<IActionResult> PutCategroies(int id, [FromBody] Categroies categroies)
        {
            if (id != categroies.CatCode)
            {
                return BadRequest();
            }

            _context.Entry(categroies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategroiesExists(id))
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

        // POST: api/Categroies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add-categories")]
        public async Task<ActionResult<Categroies>> PostCategroies([FromBody] Categroies categroies)
        {
          if (_context.Categroies == null)
          {
              return Problem("Entity set 'BanHangContext.Categroies'  is null.");
          }
            _context.Categroies.Add(categroies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostCategroies", new { id = categroies.CatCode }, categroies);
        }

        // DELETE: api/Categroies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategroies(int id)
        {
            if (_context.Categroies == null)
            {
                return NotFound();
            }
            var categroies = await _context.Categroies.FindAsync(id);
            if (categroies == null)
            {
                return NotFound();
            }

            _context.Categroies.Remove(categroies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategroiesExists(int id)
        {
            return (_context.Categroies?.Any(e => e.CatCode == id)).GetValueOrDefault();
        }
    }
}
