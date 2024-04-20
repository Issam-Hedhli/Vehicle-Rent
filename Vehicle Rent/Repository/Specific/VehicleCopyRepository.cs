using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
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

        public async Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleCopy(string vehicleId)
        {
            var vehicleCopies = await _context.Set<VehicleCopy>()
                .Where(vc => vc.IdVehicle.Equals(vehicleId))
                .ToListAsync();
            return vehicleCopies;
        }
    }
}
