using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;

namespace Vehicle_Rent.Services.VehicleCatalogue
{
	public class VehicleCatalogueService : IVehicleCatalogueService
	{
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IRentalItemRepository _ItemRepository;

		public VehicleCatalogueService(IVehicleRepository vehicleRepository, IRentalItemRepository itemRepository)
		{
			_vehicleRepository = vehicleRepository;
			_ItemRepository = itemRepository;
		}
		public async Task<List<Vehicle>> GetAllVehiclesAsync()
		{
			var allVehiclesAsync = await _vehicleRepository.GetVehiclesAsync();
			return allVehiclesAsync.ToList();
		}

		public async Task<List<Vehicle>> GetAvailableVehiclesAsync()
		{
			var allVehicles = await _vehicleRepository.GetVehiclesAsync();
			return allVehicles.Where(v => v.IsAvailable).ToList();
		}

		public async Task<Vehicle> GetVehicleByIdAsync(string vehicleId)
		{
			return await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
		}

	}
}
