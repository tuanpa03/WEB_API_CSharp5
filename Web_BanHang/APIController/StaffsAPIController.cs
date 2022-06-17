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
    public class StaffsAPIController : ControllerBase
    {
        private readonly BanHangContext _context;

        public StaffsAPIController(BanHangContext context)
        {
            _context = context;
        }

        // GET: api/StaffsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staffs>>> GetStaffs()
        {
            if (_context.Staffs == null)
            {
                return NotFound();
            }

            return await _context.Staffs.ToListAsync();
        }

        // GET: api/StaffsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staffs>> GetStaffs(int id)
        {
            if (_context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FindAsync(id);

            if (staffs == null)
            {
                return NotFound();
            }

            return staffs;
        }

        // PUT: api/StaffsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-staff/{id}")]
        public async Task<IActionResult> PutStaffs(int id, Staffs staffs)
        {
            if (id != staffs.StaffCode)
            {
                return BadRequest();
            }

            _context.Entry(staffs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffsExists(id))
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

        // POST: api/StaffsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Staffs>> PostStaffs(Staffs staffs)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'BanHangContext.Staffs'  is null.");
            }

            _context.Staffs.Add(staffs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffs", new { id = staffs.StaffCode }, staffs);
        }

        // DELETE: api/StaffsAPI/5
        [HttpDelete("delete-staff/{id}")]
        public async Task<IActionResult> DeleteStaffs(int id)
        {
            if (_context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }

            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffsExists(int id)
        {
            return (_context.Staffs?.Any(e => e.StaffCode == id)).GetValueOrDefault();
        }

        [HttpPost("register")]
        public async Task<ActionResult<Staffs>> Register(Staffs staffs)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'BanHangContext.Staffs'  is null.");
            }

            _context.Staffs.Add(staffs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffs", new { id = staffs.StaffCode }, staffs);
        }

        [HttpGet("login")]
        public async Task<ActionResult<Staffs>> Login(string email, string password)
        {
            if (_context.Staffs == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Problem("Username or password must not empty");
            }

            try
            {
                var info = await _context.Staffs.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
                if (info == null)
                {
                    return Problem("User not found");
                }

                return info;
            }
            catch (Exception)
            {
                return Problem("Unknown error");
            }
        }
    }
}
