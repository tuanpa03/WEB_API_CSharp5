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
    public class StaffsController : Controller
    {
        private readonly BanHangContext _context;

        public StaffsController(BanHangContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var banHangContext = _context.Staffs.Include(s => s.Roles);
            return View(await banHangContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Roles)
                .FirstOrDefaultAsync(m => m.StaffCode == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["RoleCode"] = new SelectList(_context.Set<Roles>(), "RoleCode", "RoleName");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffCode,RoleCode,Email,Password,StaffName,Gender,Address,PhoneNumber,Birthday,Note,Status")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleCode"] = new SelectList(_context.Set<Roles>(), "RoleCode", "RoleName", staffs.RoleCode);
            return View(staffs);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FindAsync(id);
            if (staffs == null)
            {
                return NotFound();
            }
            ViewData["RoleCode"] = new SelectList(_context.Set<Roles>(), "RoleCode", "RoleName", staffs.RoleCode);
            return View(staffs);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffCode,RoleCode,Email,Password,StaffName,Gender,Address,PhoneNumber,Birthday,Note,Status")] Staffs staffs)
        {
            if (id != staffs.StaffCode)
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
                    var jsonContent = await client.PutAsJsonAsync("api/StaffsAPI/update-staff/" + id, staffs);
                    if (jsonContent.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(staffs);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.Roles)
                .FirstOrDefaultAsync(m => m.StaffCode == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Staffs staffs)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'BanHangContext.Staffs'  is null.");
            }

            if (ModelState.IsValid)
            {
                staffs.Status = false;
                HttpClient client = new HttpClient(); //set đường dẫn cơ bản 
                client.BaseAddress = new Uri("https://localhost:7138/");
                if (ModelState.IsValid)
                {
                    var jsonContent = await client.PutAsJsonAsync("api/StaffsAPI/delete-staff/" + id, staffs);
                    if (jsonContent.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View();
        }

        public IActionResult StaffLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return View();
            }
            SearchInfo(email, password);
            HttpContext.Session.SetString("username", email);
            HttpContext.Session.SetString("password", password);

            return RedirectToAction("Index");
        }
        public static Staffs SearchInfo(string email, string password)
        {
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/");
            var JsonConnect = client.GetAsync("api/StaffsAPI/login").Result;//Trả về json
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;//trả về string
            //đọc list ddataa đối tượng 
            var model = JsonConvert.DeserializeObject<Staffs>(JsonData);
            return model;
        }


        public IActionResult StaffRegister([Bind("Email,Password,StaffName,RoleCode ,Gender,Address,PhoneNumber,Birthday,Note,Status")] Staffs _staff)
        {
            var debug = ModelState.IsValid;
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/");
            var JsonPush = client.PostAsJsonAsync("api/StaffsAPI/register", _staff).Result;//Trả về json
            if (JsonPush.IsSuccessStatusCode == true)
            {
                return RedirectToAction("StaffLogin");
            }
            return View();
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }

        private bool StaffsExists(int id)
        {
          return (_context.Staffs?.Any(e => e.StaffCode == id)).GetValueOrDefault();
        }
    }
}
