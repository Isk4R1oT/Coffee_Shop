using Coffe_Shop.Entityes;
using Coffee_Shop.Entities;
using Coffee_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coffee_Shop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Context _context;
        public OrdersController(Context context)
        {
            _context = context;
        }
        public IActionResult CreateOrder()
        {
            return View();
        }       
            

        public  IActionResult Payment(Payment payment,AdressOrder adressOrder)
        {
            
            var basket = _context.Baskets.Include(b => b.BasketItem).ThenInclude(bi => bi.Coffee)
                            .First(x => x.Client.Id == HttpContext.Session.GetInt32("UserId"));
            int orderValue = basket.BasketItem.Sum(x=>x.Coffee.Price * x.Quantity);
            return View(orderValue);
        }

        public IActionResult OrdersMenu(int id)
        {
            var client = _context.Clients.FirstOrDefault(x=>x.Id == id);
            return View(client);
        }
    }  
}
