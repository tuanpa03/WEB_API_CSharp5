using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Website_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BanHangContext _context;
        private readonly HttpClient _client = new HttpClient();

        public ProductsController(BanHangContext context)
        {
            _context = context;
            _client.BaseAddress = new Uri("https://localhost:7138/");
        }
        public IActionResult GetProducts()//show sản phẩm ra trang chủ
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7138/");
            var jsonConnect = client.GetAsync("api/ProductsAPI/get-products").Result;//kh được thì thử đuôi này xem api-get-list-sp
            string jsonData = jsonConnect.Content.ReadAsStringAsync().Result;
            //ViewData["data"] = jsonData; 
            //lấy list người từ api 
            var model = JsonConvert.DeserializeObject<List<Products>>(jsonData);
            return View(model);
        }
        public async Task<IActionResult> Index()
        {
            var jsonContent = await _client.GetAsync("api/ProductsAPI/get-products");
            var jsonData = await jsonContent.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<List<Products>>(jsonData);
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context._products == null)
            {
                return NotFound();
            }

            var fragrant = await _context._products
                .FirstOrDefaultAsync(m => m.ProductCode == id);
            if (fragrant == null)
            {
                return NotFound();
            }

            return View(fragrant);
        }

        // GET: Admin/ProductType/Create


        public IActionResult Create()
        {
            ViewData["CatCode"] = new SelectList(_context.Categroies, "CatCode", "CatName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductCode,CatCode,ProductName,Quantity,Image,Price,Note")] Products products)
        {
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/add-products");
            if (true)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatCode"] = new SelectList(_context.Categroies, "CatCode", "CatName", products.CatCode);
            return View(products);
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(Products fragrant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var myContent = JsonConvert.SerializeObject(fragrant);
        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        var byContent = new ByteArrayContent(buffer);
        //        byContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        await _client.PostAsync("api/ProductsAPI/add-products", byContent);

        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}
        // GET: Admin/ProductType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context._products == null)
            {
                return NotFound();
            }

            var fragrant = await _context._products.FindAsync(id);
            if (fragrant == null)
            {
                return NotFound();
            }
            return View(fragrant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Products products)
        {
            if (id != products.ProductCode)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    await _client.PutAsJsonAsync<Products>($"api/ProductsAPI/{id}", products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FragrantExists(products.ProductCode))
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
            return View(products);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context._products == null)
            {
                return NotFound();
            }

            var products = await _context._products
                .FirstOrDefaultAsync(m => m.ProductCode == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context._products == null)
            {
                return Problem("Entity set 'BanHangContext.Products'  is null.");
            }

            await _client.DeleteAsync($"api/ProductsAPI/{id}");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool FragrantExists(int id)
        {
            return (_context._products?.Any(e => e.ProductCode == id)).GetValueOrDefault();
        }
    }
}
