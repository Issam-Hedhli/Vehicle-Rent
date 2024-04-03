using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle_Rent.Models
{
    public class Photo
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        public string? FileName { get; set; }
        public string? Description { get; set; }
        public string? VehicleId { get; set; } 
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } 
    }
}
