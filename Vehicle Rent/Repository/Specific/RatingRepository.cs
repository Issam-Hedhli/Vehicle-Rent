using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class RatingRepository : EntityBaseRepository<Rating>, IRatingRepository
	{
		public RatingRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
