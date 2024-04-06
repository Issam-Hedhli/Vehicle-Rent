using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class RentalItemRepository : EntityBaseRepository<RentalItem>, IRentalItemRepository
	{
		public RentalItemRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
