using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
	public class VehicleCopy : IEntityBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
        public int RentalPrice { get; set; }
        public string? IdVehicle { get; set; }
		public virtual Vehicle Vehicle { get; set; } = new Vehicle();
		public virtual ICollection<RentalItem> RentalItems { get; set; } = new List<RentalItem>(); 
		public DateTime UnavailabilityStart { get; set; }
		public DateTime UnavailabilityEnd { get; set;}
	}
}
