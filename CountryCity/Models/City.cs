using System.ComponentModel.DataAnnotations;

namespace CountryCity.Models
{
    public class City
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


        public int ?Population { get; set; }

        [StringLength(50)]
        public string ?PostalCode { get; set; }


        public int CountryID { get; set; }      

        public Country CountryFK { get; set;}

        public ICollection<Town> towns { get; set; }


    }
}
