using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class VehicleRepository : EntityBaseRepository<Vehicle>, IVehicleRepository
	{
		public VehicleRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
