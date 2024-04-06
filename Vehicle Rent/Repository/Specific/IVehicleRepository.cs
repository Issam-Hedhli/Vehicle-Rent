using Vehicle_Rent.Models;

namespace Vehicle_Rent.Repository.Specific
{
	public interface IVehicleRepository
	{
		public Task <ICollection<Vehicle>> GetVehiclesAsync ();
		public Task<Vehicle> GetVehicleByIdAsync(string id);
	}
}
