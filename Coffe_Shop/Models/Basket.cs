using Coffe_Shop.Entityes;
using Coffee_Shop.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Shop.Models
{
    public class Basket 
    {
        public int Id { get; set; }

        public ICollection<BasketItem> BasketItem { get; set; } = new List<BasketItem>();

        [ForeignKey("ClientId")]
        public Client Client { get; set; }        

    }
    public class BasketItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CoffeeId")]
        public Coffee Coffee { get; set; }

        public Basket Basket { get; set; }
        public int Quantity { get; internal set; }
    }

    public class GetOnlyBasket
    {
        private static GetOnlyBasket _basket;

        private static readonly object _lock = new object();

        private readonly Basket basket;

        private static GetOnlyBasket Instanse
        {
            get
            {
                if (_basket == null)
                {
                    lock (_lock)
                    {
                        if (_basket == null)
                        {
                            _basket = new GetOnlyBasket();
                        }
                    }
                }
                return _basket;
            }
        }

        public GetOnlyBasket()
        {
            basket = new Basket();
        }

        public void AddItemToBasket(Coffee product)
        {
            basket.BasketItem.Add(new BasketItem { Coffee = product });
        }

        public Basket GetBasket()
        {
            return basket;
        }
    }
    public class BasketService
    {
        private readonly Context _context;

        public BasketService(Context context)
        {
            _context = context;
        }

        public async Task SaveBasket(Basket basket)
        {
            // Проверяем, есть ли корзина в базе данных
            var existingBasket = _context.Baskets
                .Include(b => b.BasketItem)
                .ThenInclude(bi => bi.Coffee)
                .FirstOrDefault(b => b.Id == basket.Id);

            if (existingBasket == null)
            {
                // Если корзины нет, добавляем новую
                 _context.Baskets.Add(basket);
            }
            else
            {
                // Если корзина существует, обновляем её
                _context.Entry(existingBasket).CurrentValues.SetValues(basket);

                // Обновляем элементы корзины
                foreach (var basketItem in basket.BasketItem)
                {
                    var existingBasketItem = existingBasket.BasketItem.FirstOrDefault(bi => bi.Id == basketItem.Id);
                    if (existingBasketItem == null)
                    {
                        // Если элемента нет, добавляем новый
                        existingBasket.BasketItem.Add(basketItem);
                    }
                    else
                    {
                        // Если элемент есть, обновляем его
                        _context.Entry(existingBasketItem).CurrentValues.SetValues(basketItem);
                    }
                }
            }

            // Сохраняем изменения в базе данных
            _context.SaveChangesAsync();
        }
    }
}