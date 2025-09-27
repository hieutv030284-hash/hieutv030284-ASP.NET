using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppleStoreWeb.Data;
using AppleStoreWeb.Models;

namespace AppleStoreWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var featuredProducts = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsFeatured && p.IsActive)
            .Take(8)
            .ToListAsync();

        var categories = await _context.Categories.ToListAsync();

        ViewBag.Categories = categories;
        return View(featuredProducts);
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
