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

builder.Services.AddMemoryCache();





builder.Services.AddDbContext<CountryContext>(x => x.UseSqlServer("ConnectionStrings:SqlServerConnectionString"));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CountryContext>();
builder.Services.AddMvcCore();

//builder.Services.AddIdentity<AppUser, AppRole>(_ =>
//{
//    _.Password.RequiredLength = 5; //En az kaç karakterli olması gerektiğini belirtiyoruz.
//    _.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunluluğunu kaldırıyoruz.
//    _.Password.RequireLowercase = false; //Küçük harf zorunluluğunu kaldırıyoruz.
//    _.Password.RequireUppercase = false; //Büyük harf zorunluluğunu kaldırıyoruz.
//    _.Password.RequireDigit = false; //0-9 arası sayısal karakter zorunluluğunu kaldırıyoruz.
//}).AddPasswordValidator<CustomPasswordValidation>().AddEntityFrameworkStores<AppDbContext>().AddEntityFrameworkStores<AppDbContext>();;
//services.AddMvc();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;    //Numeric karakterlere izin verilmedi.
    options.Password.RequiredLength = 5;      //En az 5 karakterli olacak.
    options.Password.RequireLowercase = true; //küçük harf zorunluluğu var.
    options.Password.RequireUppercase = false;//büyük harf zorunluluğu yok.
    options.Password.RequireNonAlphanumeric = false; //alphanumericolamayan karakter zorunlulugu yoktur.

    options.User.RequireUniqueEmail = true; //Email Adresi Artık tekil bir değerdir.

    //özel validasyonları IPasswordValidator arayuzunu kullanmamız gerekecektir.   

});

builder.Services.ConfigureApplicationCookie(_ =>
{
    _.LoginPath = new PathString("/Home/Login");
    _.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityExampleCookie", //Oluşturulacak Cookie'yi isimlendiriyoruz.
        HttpOnly = false, //Kötü niyetli insanların client-side tarafından Cookie'ye erişmesini engelliyoruz.
        /*  Expiration = TimeSpan.FromMinutes(120),*/  //Oluşturulacak Cookie'nin vadesini belirliyoruz.
        MaxAge = TimeSpan.FromMinutes(120),

        SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
        //Uygulamamıza ait Cookie bilgilerinin 3. taraflardan kaynaklanan isteklere gönderilip gönderilmemesi
        //ayarını yaptığımız bir özelliktir. "None","Strict" ve "Lax" o.ü. üç farklı değer alır.

        //None->Cookie bilgilerini 3.taraf isteğe ekler ve gönderiri.
        //Strict--> Uygulamaya ait Cookie bilgilerini 3.taraf hiçbir isteğe göndermez.
        //Lax--> Uygulamaya ait Cookie bilgilerini üst düzey(top-level) navigasyonlara
        //sebep olmayan yani bir başka deyişle adres çubuğundaki değişikliklere neden olmayan isteklere
        //göndermeyecektir.

        SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden erişilebilir yapıyoruz.
        //SecurePolicy, uygulamamıza ait Cokie bilgilerinin güvenilir(HTTPS) ya da güvensiz(HTTP)
        //üzerinden erişilebilir olup olmamasını ayarladığımız özelliktir.
        //Always--> Cookie'leri HTTPS üzerinden erişebilir yapar.
        //SameAsRequest-->Cookie'leri hem HTTP hemde HTTPS protokolu üzerinden erişilebilir
        //yapar.
        //NONE--> Cookie'leri HTTP üzerinden erişilebilir yapar.

    };
    _.SlidingExpiration = true; //Expiration süresinin yarısı kadar süre zarfında istekte bulunulursa eğer geri kalan yarısını tekrar sıfırlayarak ilk ayarlanan süreyi tazeleyecektir.
    _.ExpireTimeSpan = TimeSpan.FromMinutes(2);//CookieBuilder nesnesinde tanımlanan Expiration değerinin varsayılan değerlerle ezilme ihtimaline karşın tekrardan Cookie vadesi burada da belirtiliyor.
    _.AccessDeniedPath = new PathString("/Authority/Index"); //Yetkisi olmadan sayfaya ulasmaya calisanlari bu sayfaya yonlendiriyoruz.

});

//Bu şekilde kullanılacak olan Cookie yapılanmasının temel konfigurasyon ayarları sağlanmış oluyor.


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
