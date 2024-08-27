using Coffe_Shop.Entityes;
using Coffee_Shop.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coffee_Shop.Controllers
{
    public class AddCartProduct : Controller
    {

        private readonly Context _context;

        public AddCartProduct(Context context)
        {
           _context = context;
        }
        public async Task<IActionResult> Details(int id)
        {           
                var product = await _context.Coffees.FirstAsync(p => p.Id == id);
                return View(product);            
        }
      
    }
}
