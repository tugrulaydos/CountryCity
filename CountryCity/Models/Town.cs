using System.ComponentModel.DataAnnotations;

namespace CountryCity.Models
{
    public class Town
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


        public int ?Population { get; set; }

        public int CityID { get; set; }
        
        public City CityFK { get; set; }

        


    }
}
