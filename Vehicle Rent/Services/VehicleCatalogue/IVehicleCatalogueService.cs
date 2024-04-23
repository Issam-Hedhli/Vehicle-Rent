using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.VehicleCatalogue
{
	public interface IVehicleCatalogueService
	{
		public Task<List<Vehicle>> GetAllVehiclesAsync();
		public Task<Vehicle> GetVehicleByIdAsync(string vehicleId);
        public Task<List<Vehicle>> GetRentedVehiclesByCustomerIdAsync(string id);
        public Task<List<Vehicle>> GetReturnedVehiclesByCustomerIdAsync(string id);
        Task<VehicleCopy> GetVehicleCopyByIdAsync(string vehicleCopyId);
        Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleId(string vehicleId);
        bool IsCurrentlyRented(Vehicle vehicle, string id);
        public bool IsAlreadyRented(Vehicle vehicle, string userId);
    }
}

