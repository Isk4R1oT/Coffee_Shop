﻿// <auto-generated />
using Coffe_Shop.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coffee_Shop.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240820152432_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Coffee_Shop.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.Coffee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoffeeGrind")
                        .HasColumnType("int");

                    b.Property<int>("CoffeeType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coffees");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CoffeeId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Coffee_Shop.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Coffee_Shop.Models.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("CoffeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("CoffeeId")
                        .IsUnique();

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.Order", b =>
                {
                    b.HasOne("Coffee_Shop.Entities.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.OrderItem", b =>
                {
                    b.HasOne("Coffee_Shop.Entities.Coffee", "Coffee")
                        .WithMany("OrderItem")
                        .HasForeignKey("CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coffee_Shop.Entities.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Coffee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Coffee_Shop.Models.Basket", b =>
                {
                    b.HasOne("Coffee_Shop.Entities.Client", "Client")
                        .WithOne("Basket")
                        .HasForeignKey("Coffee_Shop.Models.Basket", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Coffee_Shop.Models.BasketItem", b =>
                {
                    b.HasOne("Coffee_Shop.Models.Basket", "Basket")
                        .WithMany("BasketItem")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coffee_Shop.Entities.Coffee", "Coffee")
                        .WithOne("BasketItem")
                        .HasForeignKey("Coffee_Shop.Models.BasketItem", "CoffeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Coffee");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.Client", b =>
                {
                    b.Navigation("Basket")
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.Coffee", b =>
                {
                    b.Navigation("BasketItem")
                        .IsRequired();

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("Coffee_Shop.Entities.Order", b =>
                {
                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("Coffee_Shop.Models.Basket", b =>
                {
                    b.Navigation("BasketItem");
                });
#pragma warning restore 612, 618
        }
    }
}
