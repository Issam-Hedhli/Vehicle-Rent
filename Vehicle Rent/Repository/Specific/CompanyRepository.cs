using Vehicle_Rent.Data;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public class CompanyRepository : EntityBaseRepository<Company>, ICompanyRepository
	{
		public CompanyRepository(CarRentalDbContext context) : base(context)
		{
		}
	}
}
