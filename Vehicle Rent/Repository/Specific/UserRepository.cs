using Microsoft.EntityFrameworkCore;
using Stripe.Terminal;
using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class UserRepository : EntityBaseRepository<User>, IUserRepository
	{
		public UserRepository(CarRentalDbContext context) : base(context)
		{
		}

        public async Task<User> GetEagerCustomerByIdAsync(string id)
        {
            var customer = await _context.Set<User>()
                .Include(r => r.Rentals)
                .ThenInclude(vc => vc.VehicleCopy)
                .ThenInclude(bc => bc.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == id);
            return customer;
        }
    }
}
