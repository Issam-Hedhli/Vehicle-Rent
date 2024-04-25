using Stripe.Terminal;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public interface IUserRepository: IEntityBaseRepository<User>
	{
        public Task<User> GetEagerCustomerByIdAsync(string id);

    }
}
