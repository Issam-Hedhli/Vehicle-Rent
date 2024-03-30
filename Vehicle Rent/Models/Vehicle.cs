using System.ComponentModel.DataAnnotations;
using Vehicle_Rent.Repository;

namespace Vehicle_Rent.Models
{
    public class Vehicle:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }
        public decimal RentalPrice { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public AvailibilityStatus Status { get; set; }
        public int AgencyId { get; set; }
        public virtual Agency Agency { get; set; }

    }
}
