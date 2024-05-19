using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.VehicleCatalogue
{
	public interface IVehicleCatalogueService
	{
		public Task<List<Vehicle>> GetAllVehiclesAsync();
		public Task<Vehicle> GetVehicleByIdAsync(string vehicleId);
        Task<VehicleCopy> GetVehicleCopyByIdAsync(string vehicleCopyId);
        Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleId(string vehicleId);
        public Task<List<VehicleCopy>> GetCurrentlyRentedVehicleCopiesByUserIdAsync(string id);
        bool IsCurrentlyRented(Vehicle vehicle, string id);
        public bool IsAlreadyRented(Vehicle vehicle, string userId);
        Task<List<VehicleCopy>> GetReturnedVehicleCopiesByCustomerIdAsync(string id);
    }
}

