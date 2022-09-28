using System.ComponentModel.DataAnnotations;

namespace CountryCity.Models
{
    public class Country
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Capital { get; set; }
       

        public int ?Population { get; set; }

        public ICollection<City> cities { get; set; }



    }
}
