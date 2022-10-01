using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountryCity.Controllers
{
    [Authorize()]
    public class PagesController : Controller
    {
        
        [Authorize(Roles = "Administrator")]
        public IActionResult Page1()
        {
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

        
    }
}
