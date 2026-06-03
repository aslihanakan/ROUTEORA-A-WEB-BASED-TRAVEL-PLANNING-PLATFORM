using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Controllers
{
    public class TripController : Controller
    {
        private readonly AppDbContext _context;

        public TripController(AppDbContext context)
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

            var trips = _context.Trips
                .Include(t => t.Destination)
                .Where(t => t.UserId == userId.Value)
                .ToList();

            return View(trips);
        }

        public IActionResult Details(int id)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var trip = _context.Trips
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == id && t.UserId == userId.Value);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        public IActionResult Create(int? destinationId, string? routeName)
        {
            if (GetCurrentUserId() == null)
                return RedirectToAction("Login", "Account");

            var trip = new Trip();

            if (destinationId.HasValue)
                trip.DestinationId = destinationId.Value;

            if (!string.IsNullOrWhiteSpace(routeName))
                trip.RouteName = routeName;

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trip trip)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            trip.UserId = userId.Value;

            if (trip.DestinationId == 0)
            {
                var routeDestination = _context.Destinations
                    .FirstOrDefault(d => d.CityName == "Suggested Route");

                if (routeDestination == null)
                {
                    routeDestination = new Destination
                    {
                        CityName = "Suggested Route",
                        CountryName = "Route",
                        Description = "Generated from suggested routes.",
                        BestSeason = "Anytime",
                        EstimatedBudget = 0,
                        ImageUrl = ""
                    };

                    _context.Destinations.Add(routeDestination);
                    _context.SaveChanges();
                }

                trip.DestinationId = routeDestination.Id;
            }

            if (ModelState.IsValid)
            {
                _context.Trips.Add(trip);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFromVisaFree(string countryName)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var destination = _context.Destinations
                .FirstOrDefault(d => d.CountryName.ToLower() == countryName.ToLower());

            if (destination == null)
            {
                destination = new Destination
                {
                    CountryName = countryName,
                    CityName = string.Empty,
                    Description = countryName,
                    BestSeason = "Anytime",
                    EstimatedBudget = 0,
                    ImageUrl = ""
                };

                _context.Destinations.Add(destination);
                _context.SaveChanges();
            }

            return RedirectToAction("Create", new { destinationId = destination.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFromRoute(string routeName)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            return RedirectToAction("Create", new { routeName = routeName });
        }

        public IActionResult Edit(int id)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var trip = _context.Trips
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == id && t.UserId == userId.Value);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Trip trip)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (id != trip.Id)
                return BadRequest();

            var existingTrip = _context.Trips
                .FirstOrDefault(t => t.Id == id && t.UserId == userId.Value);

            if (existingTrip == null)
                return NotFound();

            if (trip.DestinationId == 0)
            {
                var routeDestination = _context.Destinations
                    .FirstOrDefault(d => d.CityName == "Suggested Route");

                if (routeDestination == null)
                {
                    routeDestination = new Destination
                    {
                        CityName = "Suggested Route",
                        CountryName = "Route",
                        Description = "Generated from suggested routes.",
                        BestSeason = "Anytime",
                        EstimatedBudget = 0,
                        ImageUrl = ""
                    };

                    _context.Destinations.Add(routeDestination);
                    _context.SaveChanges();
                }

                trip.DestinationId = routeDestination.Id;
            }

            if (ModelState.IsValid)
            {
                existingTrip.DestinationId = trip.DestinationId;
                existingTrip.StartDate = trip.StartDate;
                existingTrip.EndDate = trip.EndDate;
                existingTrip.Budget = trip.Budget;
                existingTrip.TravelStyle = trip.TravelStyle;
                existingTrip.RouteName = trip.RouteName;
                existingTrip.Notes = trip.Notes;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(trip);
        }

        public IActionResult Delete(int id)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var trip = _context.Trips
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == id && t.UserId == userId.Value);

            if (trip == null)
                return NotFound();

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var trip = _context.Trips
                .FirstOrDefault(t => t.Id == id && t.UserId == userId.Value);

            if (trip != null)
            {
                _context.Trips.Remove(trip);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}