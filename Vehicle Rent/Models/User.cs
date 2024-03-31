using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string DriverLicence { get; set; }
        public string AdditionalInfo { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
