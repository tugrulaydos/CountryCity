using System.ComponentModel.DataAnnotations;

namespace CountryCity.Models
{
    public class Country
    {
        [Key]
        public int ID { get; set; }

        public string CountryName { get; set; }

        public string Capital { get; set; }

        public string ?MonetaryUnit { get; set; }

        public string ?Population { get; set; }



    }
}
