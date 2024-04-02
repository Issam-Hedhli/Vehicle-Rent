using System.ComponentModel.DataAnnotations;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
	public class Rating : IEntityBase
	{
		[Key]
		public int Id { get; set; }
		public int? Value { get; set; }
		public string? Comment { get; set; }
		public int VehicleId { get; set; }
		public virtual Vehicle Vehicle { get; set; } = new Vehicle();


	}
}
