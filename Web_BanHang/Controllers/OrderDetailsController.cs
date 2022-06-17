using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly BanHangContext _context;

        public OrderDetailsController(BanHangContext context)
        {
            _context = context;
        }

        public void Add(OrderDetails orderDetails)
        {
            if (true)
            {
                _context.Add(orderDetails);
                _context.SaveChanges();
            }
        }
        public void newEdit(int idorder, int idproduct, OrderDetails orderDetails)
        {
            if (idorder != orderDetails.OrderCode || idproduct != orderDetails.ProductCode)
            {
                return;
            }

            if (true)
            {
                try
                {
                    _context.Update(orderDetails);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsExists(orderDetails.OrderCode))
                    {
                        return;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var banHangContext = _context.OrderDetails.Include(o => o.Orders).Include(o => o.Products);
            return View(await banHangContext.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? idorder,int? idproduct)
        {
            if (idorder == null || idproduct == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .Include(o => o.Orders)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(m => m.OrderCode == idorder && m.ProductCode == idproduct);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderCode"] = new SelectList(_context.Orders, "OrderCode", "OrderCode");
            ViewData["ProductCode"] = new SelectList(_context.Set<Products>(), "ProductCode", "ProductName");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderCode,ProductCode,Quantity")] OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderCode"] = new SelectList(_context.Orders, "OrderCode", "OrderCode", orderDetails.OrderCode);
            ViewData["ProductCode"] = new SelectList(_context.Set<Products>(), "ProductCode", "ProductName", orderDetails.ProductCode);
            return View(orderDetails);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? idorder,int? idproduct)
        {
            if (idorder == null || idproduct == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails.FindAsync(idorder, idproduct);
            if (orderDetails == null)
            {
                return NotFound();
            }
            ViewData["OrderCode"] = new SelectList(_context.Orders, "OrderCode", "OrderCode", orderDetails.OrderCode);
            ViewData["ProductCode"] = new SelectList(_context.Set<Products>(), "ProductCode", "ProductName", orderDetails.ProductCode);
            return View(orderDetails);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idorder, int idproduct, [Bind("OrderCode,ProductCode,Quantity")] OrderDetails orderDetails)
        {
            if (idorder != orderDetails.OrderCode || idproduct != orderDetails.ProductCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailsExists(orderDetails.OrderCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderCode"] = new SelectList(_context.Orders, "OrderCode", "OrderCode", orderDetails.OrderCode);
            ViewData["ProductCode"] = new SelectList(_context.Set<Products>(), "ProductCode", "ProductName", orderDetails.ProductCode);
            return View(orderDetails);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? idorder, int? idproduct)
        {
            if (idorder == null || idproduct == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
                .Include(o => o.Orders)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(m => m.OrderCode == idorder && m.ProductCode == idproduct);
            if (orderDetails == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'BanHangContext.OrderDetails'  is null.");
            }
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails != null)
            {
                _context.OrderDetails.Remove(orderDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailsExists(int id)
        {
          return (_context.OrderDetails?.Any(e => e.OrderCode == id)).GetValueOrDefault();
        }
    }
}
