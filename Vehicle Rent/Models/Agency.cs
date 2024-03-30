using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class Agency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
