using Coffe_Shop.Entityes;
using Coffee_Shop.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coffee_Shop.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly Context _context;

        public RegistrationController(Context context)
        {
            _context = context;
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Authorization()
        {
            return View();
        }

        public async Task<IActionResult> RegistrationWithParameters(string name, string email, string pass)
        {
            // Проверяем, есть ли уже пользователь с таким email            
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == email);
            if (existingClient != null)
            {
                // Если пользователь уже существует, возвращаем ошибку или обрабатываем ситуацию как-то иначе
                throw new Exception($"Пользователь с email {email} уже существует.");
            }

            var client = new ClientBilder()
                .Name(name)
                .Email(email)
                .Password(pass)
                .Bild();

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AutorisationWithParameters(string name, string pass)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
            {
                // Если имя или пароль не введены, возвращаем ошибку
                return View("Error", new { message = "Пожалуйста, введите имя и пароль." });
            }

            if (_context.Clients.Any(c => c.Name == name))
            {
                    
                var client = await _context.Clients.FirstOrDefaultAsync(x => x.Name == name);

                if (PasswordHash.CheckPass(client.Id, pass))
                {
                    // Если авторизация прошла успешно, сохраняем данные пользователя в сессию
                    HttpContext.Session.SetString("UserName", client.Name);
                    HttpContext.Session.SetInt32("UserId", client.Id);

                    // Перенаправляем пользователя на страницу профиля
                    return RedirectToAction("UserProfile", "UserMenu");
                }
                else
                {
                    // Если пароль неверный, возвращаем ошибку
                    return View("Error", new { message = "Неверный пароль." });
                }
            }
            else
            {
                // Если пользователь с таким именем не найден, возвращаем ошибку
                return View("Error", new { message = "Пользователь с таким именем не найден." });
            }
        }
    }
}
