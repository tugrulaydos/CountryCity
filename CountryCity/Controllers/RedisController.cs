using CountryCity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace CountryCity.Controllers
{

    public class RedisController : Controller
    {

        //Bu class'ta Redis ile alakalı işlemler gerçekleştirilecektir.

        IDistributedCache _distributedCache;

        RedisService _redisService;

        public RedisController(IDistributedCache distributedCache, RedisService redisService)
        {
            this._distributedCache = distributedCache;
            this._redisService = redisService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult CacheString() 
        {
            _distributedCache.SetString("date", DateTime.Now.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(1200),
                SlidingExpiration = TimeSpan.FromSeconds(60)

            }); //Metinsel tarzda key-value tarzında veri depolamasını gerçekleştirne metotottur.
            return View();
        }

        public IActionResult CacheGetString() //Metinsel türde depolanmış veriden key değerine karşılık geleni 
        {//döndüren fonksiyondur.
            string value = _distributedCache.GetString("date");
            return View();
        }

        public IActionResult CacheRemove()  //Key Değeri verilen datayı siler.
        {
            _distributedCache.Remove("date");  //ilgili View'i silen metot'tur.
            return View();
        }



        public IActionResult CacheSetBinary() //Cache’de binary olarak data tutmamızı sağlayan fonksiyondur.
        {
            byte[] dateByte = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            _distributedCache.Set("date", dateByte, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(1200),
                SlidingExpiration = TimeSpan.FromSeconds(60)
            });
            return View();
        }

        public IActionResult CacheGetBinary()  //Binary olarak tutulan datayı geri binary olarak elde etmemizi sağlayan fonksiyondur.
        {
            byte[] dateByte = _distributedCache.Get("date");
            string value = Encoding.UTF8.GetString(dateByte);
            return View();
        }


        public IActionResult CacheFile()  //Resim Dosyası Cacheleme.
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resim.jpg");
            byte[] fileByte = System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("file", fileByte);
            return View();
        }

        public IActionResult CacheFileRead()
        {
            byte[] fileByte = _distributedCache.Get("file");
            return File(fileByte, "image/jpg");
        }

        //Bunoktaya Kadar esas Redis veri türlerine çok temas etmedik.
        //şimdi de StackExchange.Redis ile datalarımızı Redis türlerinde tutarak Redis’i daha hakim nasıl kullanacağımızı inceleyeceğiz.


       

       
        

    }
}
