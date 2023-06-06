using CsdsShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CsdsShop.Data;
using CsdsShop.Services;

namespace CsdsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConsignmentDbContext _context;
        public HomeController(ILogger<HomeController> logger, 
            ConsignmentDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.Items
                .OrderByDescending(i=>i.ListDate)
                .Take(10)
                .Select(i => new ItemListViewModel()
                {
                    Id = i.Id,
                    SellerId = i.SellerId,
                    ImgUrl = "http://localhost:9000/images/" + i.SellerId + "-" + i.Id + ".jpeg",
                    Name = i.Name,
                    Price = i.Price,
                    ThumbnailUrl = "",
                    Size = i.Size ?? ""
                }).ToList();
            return View(items);
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