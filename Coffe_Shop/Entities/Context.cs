using Coffee_Shop.Entities;
using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffe_Shop.Entityes
{
    public class Context : DbContext
    {
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; } 
        public DbSet<Client> Clients { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }


        public Context() : base()
        {
            
        }
        public Context(DbContextOptions<Context> options) : base(options)  { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-M0BO597\SQLEXPRESS;
                                          Initial Catalog=Coffee_System;Integrated Security=True;
                                          Encrypt=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Basket>()
            .HasOne(b => b.Client)
            .WithOne(c => c.Basket)
            .HasForeignKey<Basket>(b => b.Id);

            modelBuilder.Entity<BasketItem>()
            .HasOne(bi => bi.Coffee)
            .WithOne(c => c.BasketItem)
            .HasForeignKey<Coffee>(c => c.Id);*/

            modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItem)
            .HasForeignKey(oi => oi.Order.Id)
             .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
