using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Website_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class RolesController : Controller
    {
        private readonly BanHangContext _context;

        public RolesController(BanHangContext context)
        {
            _context = context;
        }

        // GET: Roles
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/");
            var JsonConnect = client.GetAsync("api/RolesAPI/roles").Result;//Trả về json
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;//trả về string
            //đọc list ddataa đối tượng 
            var model = JsonConvert.DeserializeObject<List<Roles>>(JsonData);
            return View(model);

        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleCode == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("RoleCode,RoleName,Description")] Roles roles)
        {
            ModelState.Remove("staffs");
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/");
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(roles);
                var jsonContent = await client.PostAsJsonAsync("api/RolesAPI/add-roles", roles);
                if (jsonContent.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleCode,RoleName,Description")] Roles roles)
        {
            if (id != roles.RoleCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ModelState.Remove("staffs");
                HttpClient client = new HttpClient();//set đường dẫn cơ bản 
                client.BaseAddress = new Uri("https://localhost:7138/");
                if (ModelState.IsValid)
                {
                    var jsonContent = await client.PutAsJsonAsync("api/RolesAPI/update-roles/" + id, roles);
                    if (jsonContent.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleCode == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'BanHangContext.Roles'  is null.");
            }

            if (ModelState.IsValid)
            {
                ModelState.Remove("staffs");
                HttpClient client = new HttpClient();//set đường dẫn cơ bản 
                client.BaseAddress = new Uri("https://localhost:7138/");
                if (ModelState.IsValid)
                {
                    var jsonContent = await client.DeleteAsync("api/RolesAPI/delete-roles/" + id);
                    if (jsonContent.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        private bool RolesExists(int id)
        {
          return (_context.Roles?.Any(e => e.RoleCode == id)).GetValueOrDefault();
        }
    }
}
