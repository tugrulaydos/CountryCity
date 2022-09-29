using CountryCity.Models;
using CountryCity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserViewModel appUserViewModel)
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
                else
                   result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));


                

                //varsayılan password validasyonlarına uygun olmayan sifreler girildigi taktirde "IdentityResult" bizlere hangi validasyonlar
                //olduguna dair "Errors" property'si ile bilgi verecektir. Bunu da "ModelState" nesnesine yukleyerek son kullaniciya hata 
                //mesaji olarak iletebiliriz.

            }

            return View();
        }


        public IActionResult Index()
        {
            return View(_userManager.Users);
        }  



    }
}
