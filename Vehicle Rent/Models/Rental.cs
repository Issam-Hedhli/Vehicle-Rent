using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; } 
        public int UserId { get; set; } 
        public virtual User User { get; set; } 
        public int VehicleId { get; set; } 
        public virtual Vehicle Vehicle { get; set; } 
    }
}
