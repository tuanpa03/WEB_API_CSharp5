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
    public class RolesAPIController : ControllerBase
    {
        private readonly BanHangContext _context;

        public RolesAPIController(BanHangContext context)
        {
            _context = context;
        }

        // GET: api/RolesAPI
        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return await _context.Roles.ToListAsync();
        }

        // GET: api/RolesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }

        // PUT: api/RolesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, Roles roles)
        {
            if (id != roles.RoleCode)
            {
                return BadRequest();
            }

            _context.Entry(roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
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

        // POST: api/RolesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("add-roles")]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
          if (_context.Roles == null)
          {
              return Problem("Entity set 'BanHangContext.Roles'  is null.");
          }
            _context.Roles.Add(roles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoles", new { id = roles.RoleCode }, roles);
        }

        // DELETE: api/RolesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolesExists(int id)
        {
            return (_context.Roles?.Any(e => e.RoleCode == id)).GetValueOrDefault();
        }
    }
}
