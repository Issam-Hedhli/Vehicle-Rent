using Vehicle_Rent.Data;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class AvailabilityStatusRepository : EntityBaseRepository<AvailibilityStatus>, IAvailabilityStatusRepository
	{
		public AvailabilityStatusRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
