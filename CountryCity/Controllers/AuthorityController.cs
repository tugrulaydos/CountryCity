using Microsoft.AspNetCore.Mvc;

namespace CountryCity.Controllers
{
    public class AuthorityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
