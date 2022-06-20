using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using NuGet.Protocol;
using Web_BanHang.Models;
using Website_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController()
        {
            
        }
        static string user = null;
        public string getuser()
        {
            if (user != null)
            {
                return user;
            }

            return null;
        }
        public IActionResult Index()
        {
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.password = HttpContext.Session.GetString("password");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        [ActionName("Login")]
        public async Task<IActionResult> LoginConfirm(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return View();
            }
            HttpClient client = new HttpClient();//set đường dẫn cơ bản 
            client.BaseAddress = new Uri("https://localhost:7138/");
            var JsonConnect = await client.GetAsync($"api/CustomersAPI/login/{email}/{password}");//Trả về json
            string JsonData = JsonConnect.Content.ReadAsStringAsync().Result;//trả về string
            //đọc list ddataa đối tượng 
            var model = JsonConvert.DeserializeObject<Customers>(JsonData);
            if (JsonConnect.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("username", email);
                HttpContext.Session.SetString("password", password);
                user = email;
                return RedirectToAction("Index");
            }

            return View();
        }
        

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterConfirm([Bind("FullName,Email,Password,PhoneNumber,Address,Gender,Status")] Customers _customers, IFormFile image)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid && image != null)
            {

                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);
                    _customers.Image = stream.ToArray();
                }
                HttpClient client = new HttpClient();//set đường dẫn cơ bản 
                client.BaseAddress = new Uri("https://localhost:7138/");
                
                var JsonPush = client.PostAsJsonAsync("api/CustomersAPI/register", _customers).Result;//Trả về json
                if (JsonPush.IsSuccessStatusCode == true)
                {
                    return RedirectToAction("Login");
                }
            }

            return View("Login");
        }
        [HttpPost, ActionName("Logout")]
        public async Task<IActionResult> LogoutConfirm()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}