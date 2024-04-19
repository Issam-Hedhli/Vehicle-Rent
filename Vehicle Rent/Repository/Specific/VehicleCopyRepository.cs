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

        //public async Task<VehicleCopy> GetEagerVehicleCopyById(string id)
        //{
        //    var vehiclecopy = await _context.VehicleCopies
        //        .Include(vc => vc.Vehicle)
        //        // zid includet
        //        .FirstOrDefaultAsync(vc => vc.Id == id);
        //    if (vehiclecopy == null)
        //    {
        //        throw new Exception();
        //    }
        //    else
        //    {
        //        return vehiclecopy;
        //    }
        //}
    }
}
