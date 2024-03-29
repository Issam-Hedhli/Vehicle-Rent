using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string DriverLicence { get; set; }

        public string AdditionalInfo { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
