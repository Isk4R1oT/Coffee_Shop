using Coffe_Shop.Controllers;
using Coffe_Shop.Entityes;
using Coffee_Shop.Controllers; // Assuming this is where your controllers reside
using Coffee_Shop.Entities;
using Coffee_Shop.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;   // Assuming this is where your entities (models) are defined

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddDbContext<Context>(options =>
        options.UseSqlServer(@"Data Source=DESKTOP-M0BO597\SQLEXPRESS;
                              Initial Catalog=Coffee_System;Integrated Security=True;
                              Encrypt=True;Trust Server Certificate=True"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Move ConfigureServices method implementation inside the builder object
void ConfigureServices(IServiceCollection services)
{
    services.AddDistributedMemoryCache();

    services.AddDbContext<Context>(options =>
        options.UseSqlServer(@"Data Source=DESKTOP-M0BO597\SQLEXPRESS;
                              Initial Catalog=Coffee_System;Integrated Security=True;
                              Encrypt=True;Trust Server Certificate=True"));    

    services.AddMvc()   
       .AddSessionStateTempDataProvider();
    services.AddSession();

    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    services.AddSingleton<ISessionStore, DistributedSessionStore>();
    
    services.AddScoped<Coffee_Shop.Models.GetOnlyBasket, Coffee_Shop.Models.GetOnlyBasket>();

    // Другие настройки сервисов
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseSession();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}

// Move Configure method implementation inside the builder object (not needed here)

app.UseHttpsRedirection();

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// You only need one MapControllerRoute call
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

/*builder.Services.AddSession();*/


app.Run();