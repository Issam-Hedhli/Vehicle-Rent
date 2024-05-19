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

        public async Task<List<VehicleCopy>> GetAllVehicleCopies()
        {
            var vehicleCopies = await _context.Set<VehicleCopy>()
                .Include(vc => vc.RentalItems)
                    .ThenInclude(ri => ri.User)
                .Include(vc => vc.RentalItems)
                    .ThenInclude(ri => ri.Ratings)
                .Include(vc => vc.Unavailabilities)
                .Include(vc => vc.Vehicle)
                .ToListAsync();
            return vehicleCopies.ToList(); ;
        }

        public async Task<VehicleCopy> GetVehicleCopyByIdAsync(string vehicleCopyId)
        {
            var vehicleCopy= await _context.Set<VehicleCopy>()
                .Include(vc => vc.RentalItems)
                    .ThenInclude(ri => ri.User)
                .Include(vc => vc.RentalItems)
                    .ThenInclude(ri => ri.Ratings)
                .Include(vc=>vc.Unavailabilities)
                .Include(vc=>vc.Vehicle)
                .FirstOrDefaultAsync(vc=>vc.Id==vehicleCopyId);
            return vehicleCopy;
        }

        public async Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleCopy(string vehicleId)
        {
            var vehicleCopies = await _context.Set<VehicleCopy>()
                .Where(vc => vc.IdVehicle.Equals(vehicleId))
                .Include(vc=>vc.RentalItems)
                    .ThenInclude(ri=>ri.User)
                .Include(vc=>vc.RentalItems)
                    .ThenInclude(ri=>ri.Ratings)
                .Include(vc=>vc.Unavailabilities)
                .Include(vc=>vc.Vehicle)
                .ToListAsync();
            return vehicleCopies;
        }
    }
}
