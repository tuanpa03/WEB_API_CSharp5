using Microsoft.AspNetCore.Mvc;

namespace Web_BanHang.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
