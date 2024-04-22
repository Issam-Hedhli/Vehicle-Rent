using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class UnavailabilityRepository : EntityBaseRepository<Unavailability>, IUnavailabilityRepository
	{
		public UnavailabilityRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
