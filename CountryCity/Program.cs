using CountryCity.Context;
using CountryCity.CustomValidations;
using CountryCity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CountryContext>(x => x.UseSqlServer("ConnectionStrings:SqlServerConnectionString"));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CountryContext>();
builder.Services.AddMvcCore();

//builder.Services.AddIdentity<AppUser, AppRole>(_ =>
//{
//    _.Password.RequiredLength = 5; //En az ka� karakterli olmas� gerekti�ini belirtiyoruz.
//    _.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunlulu�unu kald�r�yoruz.
//    _.Password.RequireLowercase = false; //K���k harf zorunlulu�unu kald�r�yoruz.
//    _.Password.RequireUppercase = false; //B�y�k harf zorunlulu�unu kald�r�yoruz.
//    _.Password.RequireDigit = false; //0-9 aras� say�sal karakter zorunlulu�unu kald�r�yoruz.
//}).AddPasswordValidator<CustomPasswordValidation>().AddEntityFrameworkStores<AppDbContext>().AddEntityFrameworkStores<AppDbContext>();;
//services.AddMvc();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;    //Numeric karakterlere izin verilmedi.
    options.Password.RequiredLength = 5;      //En az 5 karakterli olacak.
    options.Password.RequireLowercase = true; //k���k harf zorunlulu�u var.
    options.Password.RequireUppercase = false;//b�y�k harf zorunlulu�u yok.
    options.Password.RequireNonAlphanumeric = false; //alphanumericolamayan karakter zorunlulugu yoktur.

    options.User.RequireUniqueEmail = true; //Email Adresi Art�k tekil bir de�erdir.

    //�zel validasyonlar� IPasswordValidator arayuzunu kullanmam�z gerekecektir.   

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
