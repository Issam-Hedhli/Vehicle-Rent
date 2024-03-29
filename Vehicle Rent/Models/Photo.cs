using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public int VehicleId { get; set; } // Foreign key for Vehicle
        public Vehicle Vehicle { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } // Navigation property for Vehicle
    }
}
