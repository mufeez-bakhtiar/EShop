using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EshopContext _context;

        public HomeController(ILogger<HomeController> logger, EshopContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            ViewBag.category = _context.Categories.ToList();
            ViewBag.featured = _context.FeaturedProducts.ToList();
            return View();
        }

        public IActionResult Admin()
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
}