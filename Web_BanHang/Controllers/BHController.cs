using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website_BanHang.Models;
using Web_BanHang.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_BanHang.Controllers
{
    public class BHController : Controller
    { 
        BanHangContext _bhcontext;
        private Orders _hoadonBan;
        private OrderDetails _chitietHdban;
        private Products _hang;
        private OrdersController _hdBansController;
        private OrderDetailsController _chitietHdbansController;
        //private ProductsController _hangsController;
        private HomeController _homeController;
        private int makh;
        public BHController(BanHangContext context)
        {
            _bhcontext = context;
            _hdBansController = new OrdersController(_bhcontext);
            //_hangsController = new ProductsController(_bhcontext);
            _chitietHdbansController = new OrderDetailsController(_bhcontext);
            _hoadonBan = new Orders();
            _chitietHdban = new OrderDetails();
            _hang = new Products();
            _homeController = new HomeController();
            makh = 0;
        }
        public IActionResult Index()
        {
            var lsthang = _bhcontext.Products.Where(c => c.Quantity > 0).ToList();
            if (lsthang.Count >= 10)
            {
                lsthang.RemoveRange(10, lsthang.Count - 10);
            }
            return View(lsthang);
        }

        public IActionResult Products(string keystr)
        {
            if (keystr == null) keystr = "";
            return View(_bhcontext.Products.Where(c => c.ProductName.ToLower().Contains(keystr.ToLower()) && c.Quantity > 0).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Addgiohang(int id, string query)
        {
            if (_homeController.getuser()==null)
            {
                return RedirectToAction("Login","Home");
            }
            if (query == null)
            {
                TempData["mess"] = "Vui lòng không để trống";
                return RedirectToAction(nameof(Single));
            }
            foreach (char c in query)
            {
                if (!char.IsDigit(c))
                {
                    TempData["mess"] = "Vui lòng nhập số!";
                    return RedirectToAction(nameof(Single));
                }
            }

            if (Convert.ToInt32(query) < 0)
            {
                TempData["mess"] = "giá trị không hợp lệ";
                return RedirectToAction(nameof(Single));
            }

            if (_bhcontext.Products.FirstOrDefault(c => c.ProductCode == id).Quantity < Convert.ToInt32(query))
            {
                TempData["mess"] = "Số lượng vượt quá số lượng còn lại";
                return RedirectToAction(nameof(Single));
            }
            makh = _bhcontext.Customers.FirstOrDefault(c => c.Email == _homeController.getuser()).CustomerCode;
            _chitietHdban = new OrderDetails();
            if (_bhcontext.Orders.Where(c => c.CustomerCode == makh && c.Status == true).ToList().Count == 0)
            {
                themHD();
            }

            int makhadung = _bhcontext.Orders.FirstOrDefault(c => c.CustomerCode == makh && c.Status == true)
                .OrderCode;
            if (_bhcontext.OrderDetails.Where(c => c.OrderCode == makhadung && c.ProductCode == id).ToList().Count == 0)
            {
                _chitietHdban.OrderCode = makhadung;
                _chitietHdban.ProductCode = id;
                _chitietHdban.Quantity = Convert.ToInt32(query);
                _chitietHdbansController.Add(_chitietHdban);
            }
            else
            {
                _chitietHdban = _bhcontext.OrderDetails.FirstOrDefault(c => c.OrderCode == makhadung && c.ProductCode == id);
                _chitietHdban.Quantity += Convert.ToInt32(query);
                _chitietHdbansController.newEdit(_chitietHdban.OrderCode, _chitietHdban.ProductCode, _chitietHdban);
            }
            SuaTongtien(makhadung);
            TempData["mess"] = "Thêm thành công";
            return RedirectToAction(nameof(Single));
        }
        public async void themHD()
        {
            makh = _bhcontext.Customers.FirstOrDefault(c => c.Email == _homeController.getuser()).CustomerCode;
            _hoadonBan = new Orders();
            _hoadonBan.OrderDate = DateTime.Now;
            _hoadonBan.CustomerCode = makh;
            _hoadonBan.TotalMoney = 0;
            _hoadonBan.Status = true;
            _hdBansController.Create(_hoadonBan);
        }
        void SuaTongtien(int mahoadon)
        {
            _hoadonBan = _bhcontext.Orders.Find(mahoadon);
            //_hoadonBan = _context.Orders.FirstOrDefault(c => c.OrderCode == mahoadon);
            var lstproducts = _bhcontext.Products.ToList();

            foreach(var x in _bhcontext.OrderDetails.Where(p=>p.OrderCode==mahoadon))
            {
                _hoadonBan.TotalMoney += x.Quantity * lstproducts.FirstOrDefault(m=>m.ProductCode==x.ProductCode).Price;
            }
            _hdBansController.newEdit(mahoadon, _hoadonBan);
        }
        public async Task<IActionResult> Giohang()
        {
            if (_homeController.getuser() == null)
            {
                return RedirectToAction("Login", "Home");
            }
            int mhd = 0;
            makh = _bhcontext.Customers.FirstOrDefault(c => c.Email == _homeController.getuser()).CustomerCode;
            if (_bhcontext.Orders.Where(c => c.CustomerCode == makh && c.Status == true).ToList().Count() != 0)
            {
                mhd = _bhcontext.Orders.FirstOrDefault(c => c.CustomerCode == makh && c.Status == true).OrderCode;
            }

            ViewData["lsthangs"] = _bhcontext.Products.ToList();
            return View(await _bhcontext.OrderDetails.Where(c => c.OrderCode == mhd).ToListAsync());
        }
        public async Task<IActionResult> Deleteall()
        {
            var makhach = makh;
            var mhd = _bhcontext.Orders.FirstOrDefault(c => c.CustomerCode == makhach && c.Status == true).OrderCode;
            if (mhd == null) return NotFound();
            var tblcthdban = _bhcontext.OrderDetails.Where(c => c.OrderCode == mhd).ToList();
            if (tblcthdban != null)
            {
                _bhcontext.OrderDetails.RemoveRange(tblcthdban);
                await _bhcontext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Giohang));
        }


        public async Task<IActionResult> Deletegiohang(int? idorder, int? idproduct)
        {
            if (idorder == null || idproduct == null) 
            {
                return NotFound();
            }

            var tblChitietHdban = await _bhcontext.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderCode == idorder && m.ProductCode == idproduct);
            if (tblChitietHdban == null)
            {
                return NotFound();
            }
            _bhcontext.OrderDetails.Remove(tblChitietHdban);
            SuaTongtien(_bhcontext.Orders.FirstOrDefault(c => c.CustomerCode == makh && c.Status == true).OrderCode);
            await _bhcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Giohang));
        }

        //public async Task<IActionResult> Thanhtoan()
        //{
        //    makh = _bhcontext.Customers.FirstOrDefault(c => c.Email == _homeController.getuser()).CustomerCode;
        //    if (_bhcontext.Orders.Where(c => c.CustomerCode == makh && c.Status == true).Count()==0)
        //    {
        //        TempData["thongbao"] = "không có đơn hàng cần thanh toán";
        //        return RedirectToAction("Giohang");
        //    }
        //    int mhd = _bhcontext.Orders.FirstOrDefault(c => c.CustomerCode == makh && c.Status == true).OrderCode;
        //    foreach (var x in _bhcontext.OrderDetails.Where(c => c.OrderCode == mhd).ToList())
        //    {
        //        _hang = new Products();
        //        _hang = _bhcontext.Products.Where(c => c.ProductCode == x.ProductCode).FirstOrDefault();
        //        _hang.Quantity -= x.Quantity;
        //        _hangsController.newEdit(_hang.ProductCode, _hang);
        //    }
        //    _hoadonBan = _bhcontext.Orders.FirstOrDefault(c => c.OrderCode == mhd && c.Status == true);
        //    if (_hoadonBan == null) return NotFound();
        //    _hoadonBan.Status = false;
        //    _hdBansController.newEdit(_hoadonBan.OrderCode, _hoadonBan);
        //    SuaTongtien(mhd);
        //    TempData["thongbao"] = "Thanh toán thành công";
        //    return RedirectToAction("Giohang");
        //}

        static public int idhang;
        public IActionResult Single(int? id)
        {
            if (id == null)
            {
                id = idhang;
            }
            else
            {
                idhang = Convert.ToInt32(id);
            }
            var hang = _bhcontext.Products.FirstOrDefault(c => c.ProductCode == id);
            return View(hang);
        }
    }
}
