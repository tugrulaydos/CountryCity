using CountryCity.Models;
using CountryCity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CountryCity.Controllers
{
    public class HomeController : Controller
    {        
      
        //UserManager Sınıfı kullanıcı yönetiminin ana karargahıdır.

        readonly UserManager<AppUser> _userManager;

        //Şimdi de bu yapılanmaları kullanarak kullanıcı oluşturalım ve var olan kullanıcıları yönetelim.

        //Bu iş için UserManager Sınıfını kullanacağız.User Manager hangi kullanıcı türünü yöneteceğini 
        //generic özelliğinden bilmek ister.Dolayısıyla ilgili sınıfın yapısını şöyle bir inceleresek.

        //Bu sınıfa erişebeilmek için Asp.NEt Core identity mimarisinin kullanıldığı uygulamalarda 
        //Dependency Injection ile talepte bulunmamız yeterli olacaktır.


        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

        }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserViewModel appUserViewModel)
        {

            //Gerekli validasyon işlemleri yapıldıktan sonra
            //AppUserViewModel nesnesinden gelen verileri oluşturulan AppUser nesnesine transfer etmekte
            //ve ilgili AppUser nesnesini UserManager nesnesi üzerinden CreateAsync metodu aracılıyığla
            //veritabanına kaydetmekteyiz.
            
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = appUserViewModel.UserName,
                    Email = appUserViewModel.Email
                };

               

                //Burada dikkat edilmesi gereken post neticesinde gelen viewmodel içerisindeki password property'si
                //"CreateAsync" icerisinde ikinci parametreye verilerek ilgili parola'nın Hash algoritması ile şifre
                //lenerek kaydedilmesi sağlanmaktadır.

                IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Password);
                //Burada manuel olarak yapılan eşleştirmeyi Automapper kütüphanesini kullanarakta otomatik bir şekilde
                //gerçekleştirebiliriz.               
              

                if (result.Succeeded)
                    return RedirectToAction("Index");

            }

            return View();
        }


        public IActionResult Index()
        {
            return View(_userManager.Users);
        }  



    }
}
