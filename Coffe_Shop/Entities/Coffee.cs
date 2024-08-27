using Coffe_Shop.Entityes;
using Coffee_Shop.Models;

namespace Coffee_Shop.Entities
{
    public enum CoffeeType
    {
        Arabica,
        Liberica,
        Brazilian,
        Tyrkie,
        Columby
    }

    public enum CoffeeGrind
    {
        Whole,
        Ground,
        Espresso,
        Turkish
    }
    public class Coffee
    {
        public int Id { get; set; }
        public string Name { get; set; }//Название

        public string Description { get; set; }//описание

        public CoffeeGrind CoffeeGrind { get; set; }//Тип кофе- молотый/зерновой

        public int Price { get; set; }//цена

        public CoffeeType CoffeeType { get; set; }

        public bool InStock { get; set; }

        public BasketItem BasketItem { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; }

        public string GetCoffeeTypeName(int coffeeTypeId)
        {
            return Enum.GetName(typeof(CoffeeType), coffeeTypeId);
        }

        public string GetCoffeeGrindName(int coffeeGrindId)
        {
            return Enum.GetName(typeof(CoffeeGrind), coffeeGrindId);
        }


        public Coffee(CoffeeBuilder coffeeBuilder)
        {
            Name = coffeeBuilder.Name;
            Description = coffeeBuilder.Description;
            CoffeeGrind = coffeeBuilder.CoffeeGrind;
            CoffeeType = coffeeBuilder.CoffeeType;
            Price = coffeeBuilder.Price;
            InStock = coffeeBuilder.InStock;
        }
        public Coffee()
        {

        }

        public static void SeedCoffeeData(Context context)
        {
            if (!context.Coffees.Any())
            {
                var coffeeData = new List<Coffee>
        {
            new CoffeeBuilder()
                .WithName("Arabica Blend")
                .WithDescription("Rich and smooth Arabica blend")
                .WithGrind(CoffeeGrind.Ground)
                .WithType(CoffeeType.Arabica)
                .WithPrice(4)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Liberica Whole Beans")
                .WithDescription("Floral and complex Liberica whole beans")
                .WithGrind(CoffeeGrind.Whole)
                .WithType(CoffeeType.Liberica)
                .WithPrice(6)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Brazilian Dark Roast")
                .WithDescription("Deep, bold flavor of Brazilian dark roast")
                .WithGrind(CoffeeGrind.Ground)
                .WithType(CoffeeType.Brazilian)
                .WithPrice(5)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Tyrkie Espresso")
                .WithDescription("Intense and aromatic Tyrkie espresso")
                .WithGrind(CoffeeGrind.Espresso)
                .WithType(CoffeeType.Tyrkie)
                .WithPrice(4)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Columby Drip")
                .WithDescription("Smooth and balanced Columby drip coffee")
                .WithGrind(CoffeeGrind.Ground)
                .WithType(CoffeeType.Columby)
                .WithPrice(5)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Arabica Whole Beans")
                .WithDescription("Bright and fruity Arabica whole beans")
                .WithGrind(CoffeeGrind.Whole)
                .WithType(CoffeeType.Arabica)
                .WithPrice(6)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Liberica Turkish Grind")
                .WithDescription("Fine and intense Liberica Turkish grind")
                .WithGrind(CoffeeGrind.Turkish)
                .WithType(CoffeeType.Liberica)
                .WithPrice(5)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Brazilian Espresso")
                .WithDescription("Rich and creamy Brazilian espresso")
                .WithGrind(CoffeeGrind.Espresso)
                .WithType(CoffeeType.Brazilian)
                .WithPrice(4)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Tyrkie Drip")
                .WithDescription("Smooth and aromatic Tyrkie drip coffee")
                .WithGrind(CoffeeGrind.Ground)
                .WithType(CoffeeType.Tyrkie)
                .WithPrice(5)
                .InStockCoffee(true)
                .Build(),

            new CoffeeBuilder()
                .WithName("Columby Whole Beans")
                .WithDescription("Balanced and complex Columby whole beans")
                .WithGrind(CoffeeGrind.Whole)
                .WithType(CoffeeType.Columby)
                .WithPrice(6)
                .InStockCoffee(true)
                .Build()
        };

                context.Coffees.AddRange(coffeeData);
                context.SaveChanges();
            }
        }
    public class CoffeeBuilder
        {
            public int Id { get; set; }
            public string Name { get; set; }//Название

            public string Description { get; set; }//описание

            public CoffeeGrind CoffeeGrind { get; set; }//Тип кофе- молотый/зерновой

            public int Price { get; set; }//цена

            public CoffeeType CoffeeType { get; set; }

            public bool InStock { get; set; }


            public CoffeeBuilder WithName(string name)
            {
                Name = name;
                return this;
            }

            public CoffeeBuilder WithDescription(string description)
            {
                Description = description;
                return this;
            }

            public CoffeeBuilder WithGrind(CoffeeGrind grind)
            {
                CoffeeGrind = grind;
                return this;
            }

            public CoffeeBuilder WithType(CoffeeType type)
            {
                CoffeeType = type;
                return this;
            }

            public CoffeeBuilder WithPrice(int price)
            {
                Price = price;
                return this;
            }

            public CoffeeBuilder InStockCoffee(bool inStock)
            {
                InStock = inStock;
                return this;
            }

            public Coffee Build()
            {
                return new Coffee(this);
            }
        }
    }
}
