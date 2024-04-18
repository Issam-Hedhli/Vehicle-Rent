using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

public class AvailibilityStatus : IEntityBase
{
    [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }
    public string name { get; set; }
    public virtual ICollection<RentalItem> RentalItems { get; set; }
}
