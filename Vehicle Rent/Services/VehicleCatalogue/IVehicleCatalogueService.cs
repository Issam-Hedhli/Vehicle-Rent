using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.VehicleCatalogue
{
	public interface IVehicleCatalogueService
	{
		public Task<List<Vehicle>> GetAllVehiclesAsync();
		public Task<List<Vehicle>> GetAvailableVehiclesAsync();
		public Task<Vehicle> GetVehicleByIdAsync(string vehicleId);
	}
}

