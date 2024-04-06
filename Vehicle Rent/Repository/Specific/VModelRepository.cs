using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class VModelRepository : EntityBaseRepository<VModel>, IVModelRepository
	{
		public VModelRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
