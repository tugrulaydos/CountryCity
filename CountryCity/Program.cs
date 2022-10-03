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

builder.Services.ConfigureApplicationCookie(_ =>
{
    _.LoginPath = new PathString("/Home/Login");
    _.Cookie = new CookieBuilder
    {
        Name = "AspNetCoreIdentityExampleCookie", //Olu�turulacak Cookie'yi isimlendiriyoruz.
        HttpOnly = false, //K�t� niyetli insanlar�n client-side taraf�ndan Cookie'ye eri�mesini engelliyoruz.
        /*  Expiration = TimeSpan.FromMinutes(120),*/  //Olu�turulacak Cookie'nin vadesini belirliyoruz.
        MaxAge = TimeSpan.FromMinutes(120),

        SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin g�nderilmemesini belirtiyoruz.
        //Uygulamam�za ait Cookie bilgilerinin 3. taraflardan kaynaklanan isteklere g�nderilip g�nderilmemesi
        //ayar�n� yapt���m�z bir �zelliktir. "None","Strict" ve "Lax" o.�. �� farkl� de�er al�r.

        //None->Cookie bilgilerini 3.taraf iste�e ekler ve g�nderiri.
        //Strict--> Uygulamaya ait Cookie bilgilerini 3.taraf hi�bir iste�e g�ndermez.
        //Lax--> Uygulamaya ait Cookie bilgilerini �st d�zey(top-level) navigasyonlara
        //sebep olmayan yani bir ba�ka deyi�le adres �ubu�undaki de�i�ikliklere neden olmayan isteklere
        //g�ndermeyecektir.

        SecurePolicy = CookieSecurePolicy.Always //HTTPS �zerinden eri�ilebilir yap�yoruz.
        //SecurePolicy, uygulamam�za ait Cokie bilgilerinin g�venilir(HTTPS) ya da g�vensiz(HTTP)
        //�zerinden eri�ilebilir olup olmamas�n� ayarlad���m�z �zelliktir.
        //Always--> Cookie'leri HTTPS �zerinden eri�ebilir yapar.
        //SameAsRequest-->Cookie'leri hem HTTP hemde HTTPS protokolu �zerinden eri�ilebilir
        //yapar.
        //NONE--> Cookie'leri HTTP �zerinden eri�ilebilir yapar.

    };
    _.SlidingExpiration = true; //Expiration s�resinin yar�s� kadar s�re zarf�nda istekte bulunulursa e�er geri kalan yar�s�n� tekrar s�f�rlayarak ilk ayarlanan s�reyi tazeleyecektir.
    _.ExpireTimeSpan = TimeSpan.FromMinutes(2);//CookieBuilder nesnesinde tan�mlanan Expiration de�erinin varsay�lan de�erlerle ezilme ihtimaline kar��n tekrardan Cookie vadesi burada da belirtiliyor.
    _.AccessDeniedPath = new PathString("/Authority/Index"); //Yetkisi olmadan sayfaya ulasmaya calisanlari bu sayfaya yonlendiriyoruz.

});

//Bu �ekilde kullan�lacak olan Cookie yap�lanmas�n�n temel konfigurasyon ayarlar� sa�lanm�� oluyor.


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
