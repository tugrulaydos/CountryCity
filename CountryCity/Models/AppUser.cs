using CountryCity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CountryCity.Models
{
    public class AppUser:IdentityUser 
    {
        //Yapılanmada tüm sistemin tüpünüi string dışında belirleyebilmek istiyorsak 
        //"AppUser" ve "AppRole" sınıflarında id yapılanmasının tipinin geneic olarak belirtmeniz gerekmektedir.
        //ve bu context sınıfındaki tip ile tutarlı olmalıdır.
        //Çünkü varsayılan tüm yapılanma string olarak tasarlanmıştır. Aksi durumdaki tüm tipleri bu şekilde belirtmemiz gerekmektedir.

        //public string Memleket {get; set;}

        //public bool Cinsiyet {get; set;}


    }
}
