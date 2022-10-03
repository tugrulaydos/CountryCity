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

builder.Services.AddLogging();




builder.Services.AddDbContext<CountryContext>(x => x.UseSqlServer("ConnectionStrings:SqlServerConnectionString"));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CountryContext>();
builder.Services.AddMvcCore();

//builder.Services.AddIdentity<AppUser, AppRole>(_ =>
//{
//    _.Password.RequiredLength = 5; //En az kaç karakterli olmasý gerektiðini belirtiyoruz.
//    _.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunluluðunu kaldýrýyoruz.
//    _.Password.RequireLowercase = false; //Küçük harf zorunluluðunu kaldýrýyoruz.
//    _.Password.RequireUppercase = false; //Büyük harf zorunluluðunu kaldýrýyoruz.
//    _.Password.RequireDigit = false; //0-9 arasý sayýsal karakter zorunluluðunu kaldýrýyoruz.
//}).AddPasswordValidator<CustomPasswordValidation>().AddEntityFrameworkStores<AppDbContext>().AddEntityFrameworkStores<AppDbContext>();;
//services.AddMvc();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;    //Numeric karakterlere izin verilmedi.
    options.Password.RequiredLength = 5;      //En az 5 karakterli olacak.
    options.Password.RequireLowercase = true; //küçük harf zorunluluðu var.
    options.Password.RequireUppercase = false;//büyük harf zorunluluðu yok.
    options.Password.RequireNonAlphanumeric = false; //alphanumericolamayan karakter zorunlulugu yoktur.

    options.User.RequireUniqueEmail = true; //Email Adresi Artýk tekil bir deðerdir.

    //özel validasyonlarý IPasswordValidator arayuzunu kullanmamýz gerekecektir.   

});

builder.Services.ConfigureApplicationCookie(_ =>
{
    _.LoginPath = new PathString("/Home/Login");
    _.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityExampleCookie", //Oluþturulacak Cookie'yi isimlendiriyoruz.
        HttpOnly = false, //Kötü niyetli insanlarýn client-side tarafýndan Cookie'ye eriþmesini engelliyoruz.
        /*  Expiration = TimeSpan.FromMinutes(120),*/  //Oluþturulacak Cookie'nin vadesini belirliyoruz.
        MaxAge = TimeSpan.FromMinutes(120),

        SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
        //Uygulamamýza ait Cookie bilgilerinin 3. taraflardan kaynaklanan isteklere gönderilip gönderilmemesi
        //ayarýný yaptýðýmýz bir özelliktir. "None","Strict" ve "Lax" o.ü. üç farklý deðer alýr.

        //None->Cookie bilgilerini 3.taraf isteðe ekler ve gönderiri.
        //Strict--> Uygulamaya ait Cookie bilgilerini 3.taraf hiçbir isteðe göndermez.
        //Lax--> Uygulamaya ait Cookie bilgilerini üst düzey(top-level) navigasyonlara
        //sebep olmayan yani bir baþka deyiþle adres çubuðundaki deðiþikliklere neden olmayan isteklere
        //göndermeyecektir.

        SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden eriþilebilir yapýyoruz.
        //SecurePolicy, uygulamamýza ait Cokie bilgilerinin güvenilir(HTTPS) ya da güvensiz(HTTP)
        //üzerinden eriþilebilir olup olmamasýný ayarladýðýmýz özelliktir.
        //Always--> Cookie'leri HTTPS üzerinden eriþebilir yapar.
        //SameAsRequest-->Cookie'leri hem HTTP hemde HTTPS protokolu üzerinden eriþilebilir
        //yapar.
        //NONE--> Cookie'leri HTTP üzerinden eriþilebilir yapar.

    };
    _.SlidingExpiration = true; //Expiration süresinin yarýsý kadar süre zarfýnda istekte bulunulursa eðer geri kalan yarýsýný tekrar sýfýrlayarak ilk ayarlanan süreyi tazeleyecektir.
    _.ExpireTimeSpan = TimeSpan.FromMinutes(2);//CookieBuilder nesnesinde tanýmlanan Expiration deðerinin varsayýlan deðerlerle ezilme ihtimaline karþýn tekrardan Cookie vadesi burada da belirtiliyor.
    _.AccessDeniedPath = new PathString("/Authority/Index"); //Yetkisi olmadan sayfaya ulasmaya calisanlari bu sayfaya yonlendiriyoruz.

});

//Bu þekilde kullanýlacak olan Cookie yapýlanmasýnýn temel konfigurasyon ayarlarý saðlanmýþ oluyor.


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
