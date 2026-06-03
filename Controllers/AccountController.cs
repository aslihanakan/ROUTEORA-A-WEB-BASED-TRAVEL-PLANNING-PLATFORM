using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            // Zaten giriş yapmışsa ana sayfaya yönlendir
            if (HttpContext.Session.GetInt32("UserId") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // E-posta zaten kayıtlı mı?
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi zaten kayıtlı.");
                return View(model);
            }

            // Şifreyi hash'le, düz metin ASLA saklanmaz
            var user = new User
            {
                FullName = model.FullName,
                Email    = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Kayıt başarılı, giriş sayfasına yönlendir
            return RedirectToAction("Login", "Account");
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            // Kullanıcı bulunamadı veya şifre yanlış — aynı hata mesajı (enumeration saldırısı önlemi)
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId",   user.Id);
            HttpContext.Session.SetString("UserName", user.FullName);

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
