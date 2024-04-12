using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Models
{
	public class Rating : IEntityBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		public int Value { get; set; }
		public string? Comment { get; set; }
		public string? RentalId { get; set; }
		public virtual RentalItem RentalItem { get; set; }


	}
}
