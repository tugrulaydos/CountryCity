using CountryCity.Context;
using CountryCity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CountryContext>(x => x.UseSqlServer("ConnectionStrings:SqlServerConnectionString"));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CountryContext>();
builder.Services.AddMvcCore();


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
