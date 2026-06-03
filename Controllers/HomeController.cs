using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Data;
using TravelPlanner.Models;

namespace TravelPlanner.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.DestinationCount = _context.Destinations.Count();
        ViewBag.VisaFreeCount = _context.VisaFreeCountries.Count();
        ViewBag.RouteCount = _context.SuggestedRoutes.Count();

        return View();
    }

    public IActionResult TravelTips()
    {
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
}