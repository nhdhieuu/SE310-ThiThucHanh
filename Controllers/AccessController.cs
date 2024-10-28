using Microsoft.AspNetCore.Mvc;
using ThiThucHanh.Models;

namespace ThiThucHanh.Controllers
{
    public class AccessController : Controller
    {
        ShopthuchanhContext db = new ShopthuchanhContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return View();
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                var u = db.Users.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Username", u.Username);
                    HttpContext.Session.SetInt32("Role", u.Role ?? -1);

                    if (u.Role == 0)
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác!");
                }
            }
            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            if (HttpContext.Session.GetString("Username") == null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra username đã tồn tại chưa
                var existingUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username đã tồn tại!");
                    return View(user);
                }

                try
                {
                    // Thêm user mới vào database
                    db.Users.Add(user);
                    db.SaveChanges();

                    // Tự động đăng nhập sau khi đăng ký
                    HttpContext.Session.SetString("Username", user.Username);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi đăng ký: " + ex.Message);
                    return View(user);
                }
            }
            return View(user);
        }

    }
}
