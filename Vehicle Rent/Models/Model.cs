using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
	public class VModel : IEntityBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; } 
		public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
	}
}
