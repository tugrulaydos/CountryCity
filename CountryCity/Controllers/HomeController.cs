using Microsoft.AspNetCore.Mvc;

namespace CountryCity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
