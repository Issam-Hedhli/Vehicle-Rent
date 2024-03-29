using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public decimal RentalPrice { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Rental> Rentals { get; set; }

    }
}
