using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
    public class User : IdentityUser, IEntityBase
    {
        public string? Image { get; set; }
        public virtual ICollection<RentalItem>? Rentals { get; set; }
    }
}
