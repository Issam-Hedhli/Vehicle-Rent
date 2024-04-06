using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class VehicleCopyRepository : EntityBaseRepository<VehicleCopy>, IVehicleCopyRepository
	{
		public VehicleCopyRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
