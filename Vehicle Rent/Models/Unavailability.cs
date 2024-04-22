using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
    public class Unavailability : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime startDate {  get; set; }
        public DateTime endDate { get; set; }
        public virtual VehicleCopy VehicleCopy { get; set; }
        public string? vehicleCopyId { get; set; }
    }
}
