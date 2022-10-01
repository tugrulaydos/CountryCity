using CountryCity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CountryCity.Context
{
    public class CountryContext:IdentityDbContext<AppUser,AppRole,int>  
    {
        //Kullanıcılara rol vermek istiyorsak IdentityDbContext sınıfına yukarıdaki gibi AppRole sınıfını tanımlamalıyız.

        // User modelinde "AppUser" sınıfının, role modelinde ise "AppRole" sınıfının kullanılacağını
        //belirtmiş oluyoruz. 3.Parametrede ise bu yapılanmanın primary key(ID) kololarının
        //string tipte degerlerle tutulacağını bildimiş oluyor.
        //eğer int veya başka türede bir veri olmasını istiyorsak.  
        
       


        public CountryContext(DbContextOptions<CountryContext> dbContext) : base(dbContext) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-D80M3PV; database=CityCountryDB; integrated security=true;");
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Town> Towns { get; set; }

    }
}
