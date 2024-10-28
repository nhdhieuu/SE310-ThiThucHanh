using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThiThucHanh.Models;
using ThiThucHanh.ViewModels;
using X.PagedList;

namespace ThiThucHanh.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ShopthuchanhContext db = new ShopthuchanhContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {

            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Products.AsNoTracking().OrderBy(x => x.Name);
            PagedList<Product> lst = new PagedList<Product>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult ChiTietSanPham(int maSanPham)
        {
			var sanPham = db.Products.SingleOrDefault(x => x.Id == maSanPham);
			var productDetailViewModel = new ProductDetailViewModel { sanpham = sanPham };
			return View(productDetailViewModel);
		}

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
