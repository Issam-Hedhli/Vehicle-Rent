using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; } // InProgress, Completed, Cancelled
        public int UserId { get; set; } // Foreign key for User
        public User User { get; set; } // Navigation property for User
        public int VehicleId { get; set; } // Foreign key for Vehicle
        public Vehicle Vehicle { get; set; } // Navigation property for Vehicle
    
    }
}
