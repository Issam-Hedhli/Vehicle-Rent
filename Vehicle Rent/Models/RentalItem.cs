using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
    public class RentalItem : IEntityBase
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string? PaymentMethod { get; set; }
        public string? UserId { get; set; } 
        public virtual User? User { get; set; } 
        public string? VehicleCopyId { get; set; } 
        public virtual VehicleCopy VehicleCopy { get; set; } = new VehicleCopy(); 
		public string? StatusId { get; set; }
		public virtual AvailibilityStatus Status { get; set; }
        public string? RatingId { get; set; }
        public virtual Rating Ratings { get; set; }
	}
}
