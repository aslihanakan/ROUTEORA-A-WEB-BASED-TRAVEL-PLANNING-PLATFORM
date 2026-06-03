using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Data;

namespace TravelPlanner.Controllers
{
    public class DestinationController : Controller
    {
        private readonly AppDbContext _context;

        public DestinationController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var destinations = _context.Destinations.ToList();
            return View(destinations);
        }

        public IActionResult Details(int id)
        {
            var destination = _context.Destinations.FirstOrDefault(d => d.Id == id);

            if (destination == null)
                return NotFound();

            return View(destination);
        }

        public IActionResult VisaFree()
        {
            var countries = _context.VisaFreeCountries.ToList();
            return View(countries);
        }

        public IActionResult SuggestedRoutes()
        {
            var routes = _context.SuggestedRoutes.ToList();
            return View(routes);
        }
    }
}
