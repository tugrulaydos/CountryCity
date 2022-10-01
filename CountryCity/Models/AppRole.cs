using Microsoft.AspNetCore.Identity;

namespace CountryCity.Models
{
    public class AppRole:IdentityRole<int>
    {
        //Uygulamada bir rol entity'si tanımlayabilmek için ilgili sınıfın "IdentityRole" sınıfından türemesi gerekmektedir.

        public DateTime CreationDate { get; set; }


    }
}
