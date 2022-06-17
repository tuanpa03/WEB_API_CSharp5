using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Website_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BanHangContext _context;
        private readonly HttpClient _client = new HttpClient();


        public OrdersController(BanHangContext context)
        {
            _context = context;
            _client.BaseAddress = new Uri("https://localhost:7138/");

        }

        //public void Adde(Orders orders)
        //{
        //    if (true)
        //    {
        //        _context.Add(orders);
        //        _context.SaveChanges();
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Add(Orders orders)
        //{
        //    HttpClient client = new HttpClient();//set đường dẫn cơ bản 
        //    client.BaseAddress = new Uri("https://localhost:7138/");
        //    if (ModelState.IsValid)
        //    {
        //        var myContent = JsonConvert.SerializeObject(orders);
        //        var jsonContent = await client.PostAsJsonAsync("api/ApiOrders/add-order", orders);
        //        if (jsonContent.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    //return View(categroies);

        //    //if (ModelState.IsValid)
        //    //{
        //    //    var myContent = JsonConvert.SerializeObject(orders);
        //    //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    //    var byContent = new ByteArrayContent(buffer);
        //    //    byContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    //    await _client.PostAsync("api/ApiFragrants/add-fragrant", byContent);

        //    //    return RedirectToAction("Index");
        //    //}

        //    //return View();
        //}

        public void newEdit(int id, Orders orders)
        {
            if (id != orders.OrderCode)
            {
                return;
            }

            if (true)
            {
                try
                {
                    _context.Update(orders);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderCode))
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

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var banHangContext = _context.Orders.Include(o => o.Customers);
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/api/CategroiesAPI/categories");
            var JsonConnect = client.GetAsync("categories").Result;//Trả về json
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;//trả về string
            //đọc list ddataa đối tượng 
            var model = JsonConvert.DeserializeObject<List<Categroies>>(JsonData);
            return View(await banHangContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customers)
                .FirstOrDefaultAsync(m => m.OrderCode == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerCode"] = new SelectList(_context.Customers, "CustomerCode", "Address");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderCode,CustomerCode,OrderDate,TotalMoney,Note,Status")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerCode"] = new SelectList(_context.Customers, "CustomerCode", "Address", orders.CustomerCode);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerCode"] = new SelectList(_context.Customers, "CustomerCode", "Address", orders.CustomerCode);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderCode,CustomerCode,OrderDate,TotalMoney,Note,Status")] Orders orders)
        {
            if (id != orders.OrderCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderCode))
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
            ViewData["CustomerCode"] = new SelectList(_context.Customers, "CustomerCode", "Address", orders.CustomerCode);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customers)
                .FirstOrDefaultAsync(m => m.OrderCode == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'BanHangContext.Orders'  is null.");
            }
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderCode == id)).GetValueOrDefault();
        }
    }
}
