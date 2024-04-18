using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
    public class Vehicle : IEntityBase
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public string? Photo { get; set; }
        public virtual ICollection<RentalItem> Rentals { get; set; } = new List<RentalItem>();
		public string? CompanyId { get; set; } 
		public virtual Company Company { get; set; } = new Company();
		public string? VModelId { get; set; }
		public bool IsAvailable { get; set; }
		public virtual VModel VModel { get; set;} = new VModel();
		public virtual ICollection<VehicleCopy> VehicleCopies { get; set; } = new List<VehicleCopy>();

	}
}
