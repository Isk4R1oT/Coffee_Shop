using Coffe_Shop.Entityes;
using Coffee_Shop.Entities;
using Coffee_Shop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Coffee_Shop.Controllers
{
    public class BasketController : Controller
    {

        public readonly Context _context;

        private readonly GetOnlyBasket _basket;

        public BasketController(Context context)
        {
            _basket = new GetOnlyBasket();
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddItemToBasket(int id)
        {
            var product = await _context.Coffees.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Получаем текущего авторизованного клиента
            var currentClient = await _context.Clients.FirstOrDefaultAsync(c => c.Id == HttpContext.Session.GetInt32("UserId"));

            if (currentClient != null)
           
           { // Проверяем, есть ли у клиента корзина
            var basket = _context.Baskets.Include(b => b.BasketItem).ThenInclude(bi => bi.Coffee)
                            .First(x => x.Client.Id == HttpContext.Session.GetInt32("UserId"));
            if (basket == null)
            {
                // Если корзины нет, создаем новую
                basket = new Basket
                {
                    BasketItem = new List<BasketItem>(),
                    Client = currentClient
                };
            }

            // Проверяем, есть ли элемент корзины с таким же продуктом

            var basketItem = basket.BasketItem.FirstOrDefault(bi => bi.Coffee.Id == id);
            if (basketItem == null)
            {
                basketItem = new BasketItem
                {
                    Coffee = product,
                    Basket = basket,
                    Quantity = 1
                };
                basket.BasketItem.Add(basketItem);
            }
            else
            {
                // Увеличиваем количество товара в корзине
                basketItem.Quantity++;
            }

            var basketService = new BasketService(_context);
            await basketService.SaveBasket(basket);

                return RedirectToAction("AddToBasket", "Basket");               
            }
            else
            {
                return RedirectToAction("UserProfile", "UserMenu");
            }
        }        

        public async Task <IActionResult> AddToBasket()
        {
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetInt32("UserId") != null)
            {

                var basket = _context.Baskets.Include(b => b.BasketItem).ThenInclude(bi => bi.Coffee)
                            .First(x => x.Client.Id == HttpContext.Session.GetInt32("UserId"));

                return View(basket);
            }

            return View(null);

        }        
       
      /*  public async Task<IActionResult> AddToBasket(int id, string size, int price)
        {
            using (_context)
            {
                var product = await _context.Coffees.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var basket = GetOrCreateBasket();
                    var basketItem = basket.BasketItem.FirstOrDefault(c => c.Id == product.Id);
                    if (basketItem == null)
                    {
                        basket.BasketItem.Add(basketItem);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        basketItem.Coffee.Price = price;
                    }
                    _context.SaveChanges();
                }
                return Ok();
            }
        }*/

       /* private Basket GetOrCreateBasket()
        {
            var basketId = HttpContext.Session.GetString("BasketId");
            if (string.IsNullOrEmpty(basketId))
            {
                basketId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("BasketId", basketId);
                return new Basket { Id = basketId };
            }
            else
            {
                return _context.Baskets.FirstOrDefault(b => b.Id == basketId) ?? new Basket { Id = basketId };
            }
        }*/

    }
}

