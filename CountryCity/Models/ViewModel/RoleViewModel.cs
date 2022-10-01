using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CountryCity.Models.ViewModel
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Rolü boş geçmeyiniz.")]
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }

    }
}
