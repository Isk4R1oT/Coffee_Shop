using Coffe_Shop.Entityes;
using Coffe_Shop.Models;
using Coffee_Shop.Controllers;
using Coffee_Shop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Coffee_Shop.Models;

namespace Coffe_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Coffe_Shop.Entityes.Context _context;

        public HomeController(Coffe_Shop.Entityes.Context context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
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

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
        {
            try
            {
                int totalProducts = await _context.Coffees.CountAsync();
                int totalPages = (int)Math.Ceiling((decimal)totalProducts / pageSize);

                if (page < 1 || page > totalPages)
                {
                    return RedirectToAction("Index", new { page = 1, pageSize = pageSize });
                }

                var products = await _context.Coffees
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var model = new CatalogCoffee
                {
                    Coffees = products,
                    CurrentPage = page,
                    TotalPages = totalPages
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Обработка исключения
                _logger.LogError(ex, "Ошибка при загрузке продуктов");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

    }
}
