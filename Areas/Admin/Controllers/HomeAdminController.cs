using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThiThucHanh.Models;
using ThiThucHanh.Models.Authentication;
using X.PagedList;

namespace ThiThucHanh.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	[Route("admin/homeadmin")]
	public class HomeAdminController : Controller
	{
		ShopthuchanhContext db = new ShopthuchanhContext();
		[Route("")]
		[Route("index")]
        [Authentication]
        public IActionResult Index(int? page)
		{
			int pageSize = 5;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Id);
			PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
			return View(lst);
		}

		[Route("ThemSanPhamMoi")]
		[HttpGet]
		public IActionResult ThemSanPhamMoi()
		{
            ViewBag.CategoryId= new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
		}
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(sanPham);
        }
       
        [Route("SuaSanPhamMoi")]
        [HttpGet]
        public IActionResult SuaSanPhamMoi(int maSanPham)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
            var sanPham = db.Products.Find(maSanPham);
            return View(sanPham);

        }
        [Route("SuaSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPhamMoi(Product sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index", "HomeAdmin");
            }
            return View(sanPham);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int maSanPham)
        {
            TempData["Message"] = "";
            db.Remove(db.Products.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("index", "HomeAdmin");
        }

        [Route("danhmucnguoidung")]
        [Authentication]
        public IActionResult DanhMucNguoiDung(int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            // Thêm điều kiện Where để lọc người dùng có Role = 1
            var lstnguoidung = db.Users.AsNoTracking()
                                      .Where(x => x.Role == 1)
                                      .OrderBy(x => x.Id);
            PagedList<User> lst = new PagedList<User>(lstnguoidung, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemNguoiDungMoi")]
        [HttpGet]
        public IActionResult ThemNguoiDungMoi()
        {
            return View();
        }
        [Route("ThemNguoiDungMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguoiDungMoi(User nguoidung)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(nguoidung);
                db.SaveChanges();
                return RedirectToAction("danhmucnguoidung");
            }
            return View(nguoidung);
        }
        [Route("SuaNguoiDung")]
        [HttpGet]
        public IActionResult SuaNguoiDung(int maNguoiDung)
        {
            var nguoiDung = db.Users.Find(maNguoiDung);
            return View(nguoiDung);

        }
        [Route("SuaNguoiDung")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNguoiDung(User nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhmucnguoidung", "HomeAdmin");
            }
            return View(nguoiDung);
        }
        [Route("XoaNguoiDung")]
        [HttpGet]
        public IActionResult XoaNguoiDung(int maNguoiDung)
        {
            TempData["Message"] = "";
            db.Remove(db.Users.Find(maNguoiDung));
            db.SaveChanges();
            TempData["Message"] = "User đã được xóa";
            return RedirectToAction("danhmucnguoidung", "HomeAdmin");
        }
    }
}
