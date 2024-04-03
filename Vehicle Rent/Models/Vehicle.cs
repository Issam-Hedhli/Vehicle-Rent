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
        [Required]
        public decimal RentalPrice { get; set; }
        [Required]
        public virtual ICollection<Photo>? Photos { get; set; }
        public virtual ICollection<RentalItem>? Rentals { get; set; }
		public string? CompanyId { get; set; } 
		public virtual Company? Company { get; set; } 
		public string? VModelId { get; set; } 
        public virtual VModel? VModel { get; set;}
		public virtual ICollection<VehicleCopy>? VehicleCopies { get; set; }

	}
}
