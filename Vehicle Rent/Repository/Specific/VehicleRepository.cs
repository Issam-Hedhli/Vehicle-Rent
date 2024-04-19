using Microsoft.EntityFrameworkCore;
using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class VehicleRepository : EntityBaseRepository<Vehicle>, IVehicleRepository
	{
		private readonly CarRentalDbContext _context;
		public VehicleRepository(CarRentalDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Vehicle> GetVehicleByIdAsync(string id)
		{
			var vehicle = await _context.Vehicles
				.Include(v => v.Company)
				.Include(v => v.VModel)
				.Include(v => v.VehicleCopies)
					.ThenInclude(ri => ri.RentalItems)
				.FirstOrDefaultAsync(v => v.Id == id);

			if (vehicle == null)
			{
				throw new Exception("The requested Vehicle Not found!");
			}
			else
			{
				return vehicle;
			}
		}

		public async Task<ICollection<Vehicle>> GetVehiclesAsync()
		{
			var vehicles = await _context.Set<Vehicle>()
				.Include(v => v.Company)
				.Include(v=>v.VModel)
				.Include(v => v.VehicleCopies)
					.ThenInclude(ri => ri.RentalItems)
						.ThenInclude(ri=>ri.Status)
				.ToListAsync();
			return vehicles;
		}
	}
}
