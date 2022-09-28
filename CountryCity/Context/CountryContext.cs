using CountryCity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CountryCity.Context
{
    public class CountryContext:IdentityDbContext<AppUser>
    {

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
