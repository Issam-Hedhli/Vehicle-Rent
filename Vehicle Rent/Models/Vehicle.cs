using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
    public class Vehicle:IEntityBase
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        [Required]
        public decimal RentalPrice { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public AvailibilityStatus Status { get; set; }
        public int AgencyId { get; set; }
        public virtual Agency? Agency { get; set; }
		public int CompanyId { get; set; }
		public virtual Company? Company { get; set; }
		public int VModelId { get; set; }
        public virtual VModel? VModel { get; set;}
		public virtual ICollection<Rating>? Ratings { get; set; }

	}
}
