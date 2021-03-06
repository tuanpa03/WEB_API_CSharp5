using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APIAssignment
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersAPIController : ControllerBase
    {
        private readonly Web_MVC_API_Final_CSharp5Context _context;

        public CustomersAPIController(Web_MVC_API_Final_CSharp5Context context)
        {
            _context = context;
        }

        // GET: api/CustomersAPI
        [HttpGet]
        [Route("get-customer")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            return await _context.Customers.ToListAsync();
        }

        // GET: api/CustomersAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomers(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customers = await _context.Customers.FindAsync(id);

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // PUT: api/CustomersAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomers(int id, Customer customers)
        {
            if (id != customers.CustomerCode)
            {
                return BadRequest();
            }

            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/CustomersAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // DELETE: api/CustomersAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomers(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomersExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerCode == id)).GetValueOrDefault();
        }
        [HttpGet("login")]
        public async Task<ActionResult<Customer>> Login(string email, string password)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Problem("Username or password must not empty");
            }

            try
            {
                var info = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
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
        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register([FromForm]Customer customers)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'BanHangContext.Customers'  is null.");
            }
            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomers", new { id = customers.CustomerCode }, customers);
        }
    }
}

