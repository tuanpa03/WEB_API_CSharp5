using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Website_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class CategroiesController : Controller
    {
        private readonly BanHangContext _context;

        public CategroiesController(BanHangContext context)
        {
            _context = context;
        }

        // GET: Categroies
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/api/CategroiesAPI/categories");
            var JsonConnect = client.GetAsync("categories").Result;//Trả về json
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;//trả về string
            //đọc list ddataa đối tượng 
            var model = JsonConvert.DeserializeObject<List<Categroies>>(JsonData);
            return View(model);
           
        }

        // GET: Categroies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categroies == null)
            {
                return NotFound();
            }

            var categroies = await _context.Categroies
                .FirstOrDefaultAsync(m => m.CatCode == id);
            if (categroies == null)
            {
                return NotFound();
            }

            return View(categroies);
        }

        // GET: Categroies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categroies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CatCode,CatName,Image,Description")] Categroies categroies)
        {
            ModelState.Remove("products");
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/");
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(categroies);
                var jsonContent = await client.PostAsJsonAsync("api/CategroiesAPI/add-categories", categroies);
                if (jsonContent.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(categroies);
        }

        // GET: Categroies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categroies == null)
            {
                return NotFound();
            }

            var categroies = await _context.Categroies.FindAsync(id);
            if (categroies == null)
            {
                return NotFound();
            }
            return View(categroies);
        }

        // POST: Categroies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatCode,CatName,Image,Description")] Categroies categroies)
        {
            if (id != categroies.CatCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ModelState.Remove("products");
                HttpClient client = new HttpClient();//set đường dẫn cơ bản 
                client.BaseAddress = new Uri("https://localhost:7138/");
                if (ModelState.IsValid)
                {
                    var jsonContent = await client.PutAsJsonAsync("api/CategroiesAPI/update-categories/" + id, categroies);
                    if (jsonContent.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(categroies);
        }

        // GET: Categroies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categroies == null)
            {
                return NotFound();
            }

           if (ModelState.IsValid)
            {
                ModelState.Remove("products");
                HttpClient client = new HttpClient();//set đường dẫn cơ bản 
                client.BaseAddress = new Uri("https://localhost:7138/");
                if (ModelState.IsValid)
                {
                    var jsonContent = await client.DeleteAsync("api/CategroiesAPI/update-categories/" + id);
                    if (jsonContent.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View();
        }

        // POST: Categroies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categroies == null)
            {
                return Problem("Entity set 'BanHangContext.Categroies'  is null.");
            }
            var categroies = await _context.Categroies.FindAsync(id);
            if (categroies != null)
            {
                _context.Categroies.Remove(categroies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategroiesExists(int id)
        {
          return (_context.Categroies?.Any(e => e.CatCode == id)).GetValueOrDefault();
        }
    }
}
