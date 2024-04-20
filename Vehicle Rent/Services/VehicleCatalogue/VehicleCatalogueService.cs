﻿using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;

namespace Vehicle_Rent.Services.VehicleCatalogue
{
	public class VehicleCatalogueService : IVehicleCatalogueService
	{
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IRentalItemRepository _ItemRepository;
		private readonly IUserRepository _userRepository;
        private readonly IVehicleCopyRepository _vehicleCopyRepository;

        public VehicleCatalogueService(IVehicleRepository vehicleRepository, IRentalItemRepository itemRepository, IUserRepository userRepository, IVehicleCopyRepository vehicleCopyRepository)
        {
            _vehicleRepository = vehicleRepository;
            _ItemRepository = itemRepository;
            _userRepository = userRepository;
            _vehicleCopyRepository = vehicleCopyRepository;
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

        public async Task<List<Vehicle>> GetRentedVehiclesByCustomerIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Customer is NULL or EMPTY", nameof(id));

            var customer = await _userRepository.GetEagerCustomerByIdAsync(id);
            var vehicles = customer?.Rentals
                .Where(bi => bi.StatusId == "1")
                .Select(r => r.VehicleCopy?.Vehicle)
                .ToList() ?? new List<Vehicle>();

            return vehicles;
        }

        public async Task<List<Vehicle>> GetReturnedVehiclesByCustomerIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Reader ID cannot be null or empty", nameof(id));

            var user = await _userRepository.GetEagerCustomerByIdAsync(id);
            var vehicles = user?.Rentals
                .Where(bi => bi.StatusId == "1")
                .Select(r => r.VehicleCopy?.Vehicle)
                .ToList() ?? new List<Vehicle>();

            return vehicles;
        }

        public async Task<Vehicle> GetVehicleByIdAsync(string vehicleId)
		{
			return await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
		}

        public async Task<VehicleCopy> GetVehicleCopyByIdAsync(string vehicleCopyId)
        {
            return await _vehicleCopyRepository.GetByIdAsync(vehicleCopyId,vc=>vc.RentalItems);
        }

        public async Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleId(string vehicleId)
        {
            return await _vehicleCopyRepository.GetVehiclesCopiesByVehicleCopy(vehicleId);
        }
    }
}
