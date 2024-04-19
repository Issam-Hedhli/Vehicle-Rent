
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;

namespace Vehicle_Rent.Services.VehicleRent
{
	public class RentalService : IRentalService
	{
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IVehicleCopyRepository _vehicleCopyRepository;
		private readonly IRentalItemRepository _rentalItemRepository;
		private readonly IUserRepository _userRepository;
        private readonly IAvailabilityStatusRepository _availabilityStatusRepository;
        public RentalService(IVehicleRepository vehicleRepository, IRentalItemRepository rentalItemRepository, IUserRepository userRepository, IVehicleCopyRepository vehicleCopyRepository, IAvailabilityStatusRepository availabilityStatusRepository)
        {
            _vehicleRepository = vehicleRepository;
            _rentalItemRepository = rentalItemRepository;
            _userRepository = userRepository;
            _vehicleCopyRepository = vehicleCopyRepository;
            _availabilityStatusRepository = availabilityStatusRepository;
        }
        public async Task RentVehicleCopy(string vehicleCopyId, string userId,DateTime startDate,DateTime endDate)
        {
			var vehicleCopy = await _vehicleCopyRepository.GetByIdAsync(vehicleCopyId);
            var user = await _userRepository.GetByIdAsync(userId);
            var statusborrowed = await _availabilityStatusRepository.GetByIdAsync("1");
            var rentalItem = new RentalItem()
            {
                StartDate=startDate,
                EndDate=endDate,
                User = user,
                VehicleCopy=vehicleCopy,
                Status=statusborrowed
            };
            await _rentalItemRepository.AddAsync(rentalItem);
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleCopy.IdVehicle);
            var vehiclecopies = vehicle.VehicleCopies;
            var validvehiclecopies = vehiclecopies.Where(vc => !vc.RentalItems.Any(ri => ri.StatusId == "1")).ToList();
            var isAvailable = validvehiclecopies.Any();
            vehicle.IsAvailable = isAvailable;
            await _vehicleRepository.UpdateAsync(vehicleCopy.IdVehicle, vehicle);

        }

        public Task ReturnVehicleCopy(string rentalId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

