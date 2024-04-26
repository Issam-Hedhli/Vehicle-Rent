using Vehicle_Rent.Models;
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

        public async Task<List<VehicleCopy>> GetCurrentlyRentedVehicleCopiesByUserIdAsync(string id)
        {
            User user = await _userRepository.GetEagerCustomerByIdAsync(id);
            var rentalItems = user.Rentals;
            var vehicleCopies = rentalItems.Select(ri=>ri.VehicleCopy).ToList();
            return vehicleCopies.Where(vc => vc.RentalItems.Any(ri => ri.StatusId == "1")).ToList();
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
            return await _vehicleCopyRepository.GetVehicleCopyByIdAsync(vehicleCopyId);
        }

        public async Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleId(string vehicleId)
        {
            return await _vehicleCopyRepository.GetVehiclesCopiesByVehicleCopy(vehicleId);
        }

        public bool IsAlreadyRented(Vehicle vehicle, string userId)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            var userIds = vehicle.VehicleCopies
                .SelectMany(bc => bc.RentalItems)
                .Where(bi => bi.StatusId == "1")
                .Select(bi => bi.UserId)
                .ToList();

            return userIds.Contains(userId);
        }

        public bool IsCurrentlyRented(Vehicle vehicle, string id)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            var userIds = vehicle.VehicleCopies
                .SelectMany(bc => bc.RentalItems)
                .Where(bi => bi.StatusId == "1")
                .Select(bi => bi.UserId)
            .ToList();

            return userIds.Contains(id);
        }
    }
}
