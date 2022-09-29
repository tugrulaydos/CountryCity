using CountryCity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CountryCity.Models
{
    public class AppUser:IdentityUser 
    {

        //Identity mekanizmasında kullanıcıya dair default gelen validasyonlar mevcuttur.
        //Bu Validasyonlardan "AllowedUserNameCharacters" kullanıcı adına özel hangi karakterleri
        //barındırabileceğini tutmakta ve kullanıcı adı değeri havuzda eşleşmeyen herhangi bir karakter
        //barındırıyorsa hata fırlatmaktadır.
        //Bu default validasyonlara nasıl müdahale edildiğine dair konfigurasyonuda "RequiredUniqueEmail" property'si
        //tutmaktadır.
        //Identity mimarisinde UserNaeme alanı değiştirilemez bir şekilde default olarak unique'dir.


        //Yapılanmada tüm sistemin tüpünüi string dışında belirleyebilmek istiyorsak 
        //"AppUser" ve "AppRole" sınıflarında id yapılanmasının tipinin geneic olarak belirtmeniz gerekmektedir.
        //ve bu context sınıfındaki tip ile tutarlı olmalıdır.
        //Çünkü varsayılan tüm yapılanma string olarak tasarlanmıştır. Aksi durumdaki tüm tipleri bu şekilde belirtmemiz gerekmektedir.

        //public string Memleket {get; set;}

        //public bool Cinsiyet {get; set;}


    }
}
