using CountryCity.Models;
using CountryCity.Models.ViewModel;
using CountryCity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Configuration;

namespace CountryCity.Controllers
{
    public class HomeController : Controller
    {        
      
        //UserManager Sınıfı kullanıcı yönetiminin ana karargahıdır.

        readonly UserManager<AppUser> _userManager;

        readonly SignInManager<AppUser> _signInManager;
        //Şimdi de bu yapılanmaları kullanarak kullanıcı oluşturalım ve var olan kullanıcıları yönetelim.

        //Bu iş için UserManager Sınıfını kullanacağız.User Manager hangi kullanıcı türünü yöneteceğini 
        //generic özelliğinden bilmek ister.Dolayısıyla ilgili sınıfın yapısını şöyle bir inceleresek.

        //Bu sınıfa erişebeilmek için Asp.NEt Core identity mimarisinin kullanıldığı uygulamalarda 
        //Dependency Injection ile talepte bulunmamız yeterli olacaktır.

            //Sıgn In Manager Sınıfı-->Kullanıcının giriş ve çıkışlarını kontrol eden bir sınıftır.
            //Devamında ise kullanıcı tarafından girilen email adresinin yanlış olma 
            //durumunda ModelState’e error olarak ilgili hata mesajları
            //eklenmekte ve böylece kullanıcıya bilgi verilmektedir.

        readonly ILogger<HomeController> logger;

        IMemoryCache _memoryCache;


        public HomeController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ILogger<HomeController> logger,IMemoryCache memoryCache)
        {
            _userManager = userManager;

            _signInManager = signInManager;

            this.logger = logger;
            this._memoryCache = memoryCache; 
            

        }

        public IActionResult Login(string ReturnUrl)
        {

            TempData["ReturnUrl"] = ReturnUrl;  //hangi sayfaya girilmek isteniyorsa otamatik olarak Returnrl parametresiyle gelecektir.
            return View();

            //Burada Login Action'ın get metodunda "Return Url" parametresi alınmış ve TempDate kontrolune atanmıştır.
            //Bunun nedeni, Identity mekanizması herhangi bir kulanıcının yetkisinin olmadığı sayfalara erişmeye 
            //yahut yetkisi dışında bir iş yapmaya çalıştığında otomatik olarak direkt olarak Login Action'a yönlendirilecektir.

            //İşte bu actionda veritabanıyla tutarlı veriler eşliğinde bir doğrulama gerçekleştirilirse eğer kullanıcıyı ilk gitmek
            //istediği adrese yönlendirmekteyiz.

            
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            logger.LogDebug("aydos");
            if (ModelState.IsValid)
            {
                
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    //İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, model.Lock);
                    //PasswordSignInAsync metodunun 3.parametresine true verildiği taktirde oluşturulacak cookie değerinin
                    //Expiration olarak belirtilen vade kadar tutulacağını ifade etmekte aksi taktirde session açık kaldığı
                    //sürece coockie değerlerinin kullanılabileceğini lakin browser kapatıldığı vakit cookielerin temizle
                    //neceğini ifade etmektedir.. 4. parametrede ise başarısız neticelenen n adet giriş denemelerinde
                    //ilgili kullanıcının hesabının kilitlenip kilitlenmeme durumunu kontrol etmiş oluyoruz. 

                    if (result.Succeeded)
                        return Redirect(TempData["returnUrl"].ToString());


                }
                else
                {
                    ModelState.AddModelError("NotUser", "Böyle bir kullanıcı bulunmamaktadır."); 
                    ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            logger.LogDebug("aydos");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
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
                   result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description)); //-->tüm hatlar modelstate nesnesine eklenmiştir.
                   //IdentityResult nesnesinde gelen Errors proporty'sindeki tüm hatalar. 
                   //ModelState nesnesine AddModelError metoduyla eklenmiştir.
                   //kullanıcı girmiş olduğu tüm hatalı şifreler yüzenden bilgilendirilecektir.                

                //varsayılan password validasyonlarına uygun olmayan sifreler girildigi taktirde "IdentityResult" bizlere hangi validasyonlar
                //olduguna dair "Errors" property'si ile bilgi verecektir. Bunu da "ModelState" nesnesine yukleyerek son kullaniciya hata 
                //mesaji olarak iletebiliriz.

            }

            return View();
        }

        //Cookie bazlı kimlik doğrulamasını tam olarak inşa ettikten sonra sayfa bazlı yetki kontrolu gerceklestirmemiz yeterli ve yerinde olacaktır.
        //Bunun için Authorize attributeunun kullanılması yeterlidir.Burada örneklendirme için 
        //“Index” actionını seçiyorum ve aşağıda olduğu gibi Authorize attribute ile işaretliyorum.



        [Authorize]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }  



    }
}
