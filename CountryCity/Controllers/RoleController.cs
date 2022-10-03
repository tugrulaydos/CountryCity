using CountryCity.Models;
using CountryCity.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CountryCity.Controllers
{
    public class RoleController : Controller
    {
        readonly RoleManager<AppRole> _roleManager; //Role olusturmak için kullanacagiz.

        readonly UserManager<AppUser> _userManager;

        readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ILogger<RoleController> logger)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task<IActionResult> CreateRole()
        {
            _logger.LogDebug("aydos");
            //if (id != null)
            //{
            //    AppRole role = await _roleManager.FindByIdAsync(id);

            //    return View(new RoleViewModel
            //    {
            //        Name = role.Name
            //    });
            //}
            return View();
        }

        //Role Ataması yapma
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            _logger.LogDebug("aydos");
            IdentityResult result = null;

            //if (id != null)
            //{
            //    AppRole role = await _roleManager.FindByIdAsync(id);
            //    role.Name = model.Name;
            //    result = await _roleManager.UpdateAsync(role);  //Rol guncellemek için kullanıyoruz.
            //}
            //else
            //{
            //    //Role oluşturmak için _roleManager sınıfının Create metodunu kullanıyoruz.
            //    result = await _roleManager.CreateAsync(new AppRole { Name = model.Name, CreationDate = DateTime.Now });
            //}

            result = await _roleManager.CreateAsync(new AppRole { Name = model.Name, CreationDate = DateTime.Now });

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Role");
            }


            return View();

        }

        //Rolleri Silme
        public async Task<IActionResult> DeleteRole(string id)
        {
            _logger.LogDebug("aydos");
            AppRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role); //Role Silmek için delete metodu.

            if (result.Succeeded)
            {
                //Succed
            }

            return RedirectToAction("Index");
        }

        //Rolleri Listeleme.
        public IActionResult Index()
        {
            _logger.LogDebug("aydos");

            return View(_roleManager.Roles.ToList());
        }


        //Kullanıcılara Rol Atamak
        public async Task<IActionResult> RoleAssign(string id)
        {
            _logger.LogDebug("aydos");

            AppUser user = await _userManager.FindByIdAsync(id); //ID'si eşleşen kullanıcıyı getir.

            List<AppRole> allRoles = _roleManager.Roles.ToList(); //Tüm rollleri Listeliyoruz. ve ardıdnan
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>; //o an yetki atanacak olan kullanıcının mevcut tüm rollerini elde edip
            //ardından bu bilgileri  RoleAssignView model isimli viewmodel nesnelerine atıyoruz.


            List<RoleAssignViewModel> assignRoles = new List<RoleAssignViewModel>();

            allRoles.ForEach(role => assignRoles.Add(new RoleAssignViewModel
            {
                HasAssign = userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }));
            return View(assignRoles);

        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> modelList, string id)
        {
            _logger.LogDebug("aydos");

            AppUser user = await _userManager.FindByIdAsync(id);
            foreach (RoleAssignViewModel role in modelList)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.RoleName);   //AddToRole metodu ile kullanıcıya Role atanıyor.
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }


            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> RoleUpdate(string id)
        {
            _logger.LogDebug("aydos");

            AppRole role = await _roleManager.FindByIdAsync(id);

            RoleViewModel rwm = new RoleViewModel
            {
                Name = role.Name
            };

            return View(rwm);

        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleViewModel rwm, string id)
        {
            _logger.LogDebug("aydos");
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = rwm.Name;
            await _roleManager.UpdateAsync(role);  //Rol guncellemek için kullanıyoruz.
            return RedirectToAction("Index", "Role");

        }

        //Kullanıcıya dair view’de yapılan seçim neticesinde eklenen roller 32. satırda
        //olduğu gibi “AddToRoleAsync” metodu ile kullanıcıyla
        //ilişkilendiriliyor yahut alınan yetkiler ise 34. satırda “RemoveFromRoleAsync”
        //metoduyla kullanıcıdan siliniyor.







    }
}
