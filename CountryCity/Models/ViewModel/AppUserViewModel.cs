using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace CountryCity.ViewModel
{
    public class AppUserViewModel
    {
        [Required(ErrorMessage = "Lütfen Kullanıcı Adını Boş Geçmeyiniz...")]
        [StringLength(15,ErrorMessage="Lütfen kullanıcı adını 4 ile 15 karakter arasında giriniz.",MinimumLength = 4)]
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }


        [Required(ErrorMessage ="Lüten emaili boş geçmeyiniz...")]
        [EmailAddress(ErrorMessage ="Lütfen email formatında bir değer belirtiniz")]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Lütfen şifreyi boş geçmeyiniz...")]
        [DataType(DataType.Password,ErrorMessage = "Lütfen şifreyi tüm kuralları göze alarak giriniz...")]
        public string Password { get; set; }


    }
}
