using CountryCity.Context;
using CountryCity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CountryContext>(x => x.UseSqlServer("ConnectionStrings:SqlServerConnectionString"));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CountryContext>();
builder.Services.AddMvcCore();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;    //Numeric karakterlere izin verilmedi.
    options.Password.RequiredLength = 5;      //En az 5 karakterli olacak.
    options.Password.RequireLowercase = true; //küçük harf zorunluluðu var.
    options.Password.RequireUppercase = false;//büyük harf zorunluluðu yok.
    options.Password.RequireNonAlphanumeric = false; //alphanumericolamayan karakter zorunlulugu yoktur.

    //özel validasyonlarý IPasswordValidator arayuzunu kullanmamýz gerekecektir.   

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
