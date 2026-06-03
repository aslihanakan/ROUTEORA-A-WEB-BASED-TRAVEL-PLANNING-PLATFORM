using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Controllers
{
    public class WishlistController : Controller
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        private int? GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("UserId");
        }

        public IActionResult Index()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var wishlist = _context.WishlistItems
                .Include(w => w.Destination)
                .Where(w => w.UserId == userId.Value)
                .ToList();

            return View(wishlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int destinationId)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            bool exists = _context.WishlistItems
                .Any(w => w.UserId == userId.Value && w.DestinationId == destinationId && w.ItemType == "Destination");

            if (!exists)
            {
                _context.WishlistItems.Add(new WishlistItem
                {
                    UserId = userId.Value,
                    DestinationId = destinationId,
                    ItemType = "Destination"
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddVisaFree(string countryName)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(countryName))
                return RedirectToAction("VisaFree", "Destination");

            bool exists = _context.WishlistItems
                .Any(w => w.UserId == userId.Value && w.VisaFreeCountryName == countryName && w.ItemType == "VisaFree");

            if (!exists)
            {
                _context.WishlistItems.Add(new WishlistItem
                {
                    UserId = userId.Value,
                    VisaFreeCountryName = countryName,
                    ItemType = "VisaFree"
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRoute(string routeName)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(routeName))
                return RedirectToAction("SuggestedRoutes", "Destination");

            bool exists = _context.WishlistItems
                .Any(w => w.UserId == userId.Value && w.RouteName == routeName && w.ItemType == "Route");

            if (!exists)
            {
                _context.WishlistItems.Add(new WishlistItem
                {
                    UserId = userId.Value,
                    RouteName = routeName,
                    ItemType = "Route"
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var item = _context.WishlistItems
                .FirstOrDefault(w => w.Id == id && w.UserId == userId.Value);

            if (item != null)
            {
                _context.WishlistItems.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
