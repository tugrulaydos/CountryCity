using System.ComponentModel.DataAnnotations;

namespace CountryCity.Models
{
    public class City
    {
        [Key]
        public int ID { get; set; }

        public string CityName { get; set; }

        public string ?Population { get; set; }

        public string ?PostalCode { get; set; }

        public int CountryID { get; set; }      


    }
}
