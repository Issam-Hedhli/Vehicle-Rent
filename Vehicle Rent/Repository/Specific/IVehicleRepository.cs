using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
	public interface IVehicleRepository : IEntityBaseRepository<Vehicle>
	{
		public Task <ICollection<Vehicle>> GetVehiclesAsync ();
		public Task<Vehicle> GetVehicleByIdAsync(string id);
	}
}
