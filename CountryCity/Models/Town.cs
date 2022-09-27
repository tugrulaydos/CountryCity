using System.ComponentModel.DataAnnotations;

namespace CountryCity.Models
{
    public class Town
    {
        [Key]
        public int ID { get; set; }

        public string CountryName { get; set; }

        public string ?Population { get; set; }

        public int CityID { get; set; }             


    }
}
