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
	}
}
