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
    public class ProductsAPIController : ControllerBase
    {
        private readonly BanHangContext _context;

        public ProductsAPIController(BanHangContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api-get-list-sp")]
        public List<Products> GetListProducts()
        {
            List<Products> result = _context._products.Where(p => p.Quantity > 0).ToList();
            return result;
        }
        // GET: api/ProductsAPI
        [HttpGet]
        [Route("get-products")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
          if (_context._products == null)
          {
              return NotFound();
          }
            return await _context._products.ToListAsync();
        }

        // GET: api/ProductsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
          if (_context._products == null)
          {
              return NotFound();
          }
            var products = await _context._products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/ProductsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.ProductCode)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/ProductsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("add-products")]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
          if (_context._products == null)
          {
              return Problem("Entity set 'BanHangContext.Products'  is null.");
          }
            _context._products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductCode }, products);
        }

        // DELETE: api/ProductsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            if (_context._products == null)
            {
                return NotFound();
            }
            var products = await _context._products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context._products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(int id)
        {
            return (_context._products?.Any(e => e.ProductCode == id)).GetValueOrDefault();
        }
    }
}
