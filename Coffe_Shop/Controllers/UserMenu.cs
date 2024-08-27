using Coffe_Shop.Entityes;
using Coffee_Shop.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Coffee_Shop.Controllers
{
    public class UserMenu : Controller
    {
        private readonly Context _context;

        public UserMenu(Context context)
        {
            _context = context;
        }
        public async Task <IActionResult> UserProfile()
        {
            // Проверяем, есть ли данные пользователя в сессии
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetInt32("UserId") != null)
            {
                // Получаем данные пользователя из сессии
                var userName = HttpContext.Session.GetString("UserName");
                var userId = HttpContext.Session.GetInt32("UserId");

                // Находим клиента в базе данных и передаем его в представление

                
                   var client = await _context.Clients.FindAsync((int)userId);
                   return View(client);                                                
            }
            else
            {
                // Если данных пользователя в сессии нет, перенаправляем на страницу авторизации
                return RedirectToAction("Registration", "Registration");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateProfileDetails(Client updatedClient)
        {
            if (ModelState.IsValid)
            {
                // Обновление данных клиента в базе данных
                _context.Clients.Update(updatedClient);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "UserMenu", new { Client = updatedClient });
            }

            return View("EditProfile", updatedClient);
        }

        /*public async Task<IActionResult> UserOrders()
        {
            var orders = await _context.Orders.Where(o => o.ClientId == User.Identity.GetUserId()).ToListAsync();
            return View(orders);
        }*/
    }
}
