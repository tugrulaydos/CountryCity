using CountryCity.Context;
using CountryCity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CountryCity.Controllers
{
    [Authorize()]
    public class PagesController : Controller
    {

        IMemoryCache _memoryCache;

        CountryContext _countryContext;

        public PagesController(IMemoryCache memoryCache,CountryContext countryContext)
        {
            this._memoryCache = memoryCache;

            this._countryContext = countryContext;

           
        }

        public void Set()
        {
            var CountryList = _countryContext.Countries.ToList();

            _memoryCache.Set("Countries", CountryList,new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30), //Cache'deki datanın ömrü

                SlidingExpiration = TimeSpan.FromSeconds(10), //10 sn içerisinde istek yapılırsa cache'de kalma süresi 10 sn daha artacaktır.
                Priority = CacheItemPriority.Normal
            });; //(key, Veriler,, Ayarlamalar)
           
        }

        



        [Authorize(Roles = "Administrator")]
        public IActionResult Page1()
        {                      

            if (_memoryCache.TryGetValue<List<Country>>("Countries",out List<Country> value))
            {
                var C = _memoryCache.Get<List<Country>>("Countries");

                return View(C);
            }          


            return View();
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Page2()
        {

            
            return View();
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult Page3()
        {
            
            return View();
        }

        [Authorize(Roles="Administrator")]
        public IActionResult CacheTetikle()
        {
            Set();
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ResetCache()
        {
            _memoryCache.Remove("Countries");
            return View();

        }

        
    }
}
