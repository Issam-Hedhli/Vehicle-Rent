using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

public class AvailibilityStatusWrapper : IEntityBase
{
    [Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id { get; set; }
    public AvailibilityStatus Status { get; set; }
}
