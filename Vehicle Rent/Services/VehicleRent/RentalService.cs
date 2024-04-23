
using Stripe;
using Stripe.Terminal;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.ViewModels.ReturnVehicle;

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

        public async Task ReturnVehicleCopy(ReturnVehicleVM returnVehicleVM, string userId)
        {
            if (returnVehicleVM == null)
                throw new ArgumentNullException(nameof(returnVehicleVM));

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            // Retrieve the vehicle copy based on the information provided in ReturnVehicleVM
            var vehicleCopyId = returnVehicleVM.VehicleDetailVM?.Id;
            var vehicleCopy = await _vehicleCopyRepository.GetByIdAsync(vehicleCopyId);

            if (vehicleCopy == null)
                throw new InvalidOperationException($"Vehicle copy with ID '{vehicleCopyId}' not found.");

            var rentalItem = vehicleCopy.RentalItems.FirstOrDefault(ri => ri.UserId == userId && ri.StatusId == "1"); 

            if (rentalItem == null)
            {
                throw new InvalidOperationException("No active rental found for the specified vehicle copy and user.");
            }

            rentalItem.EndDate = DateTime.Now;
            rentalItem.StatusId = "2"; 

            await _rentalItemRepository.UpdateAsync(rentalItem.Id, rentalItem);

            var vehicleId = vehicleCopy.IdVehicle;
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);

            var activeRentalsExist = vehicle.VehicleCopies.Any(vc => vc.Id != vehicleCopyId && vc.RentalItems.Any(ri => ri.StatusId == "1"));

            vehicle.IsAvailable = !activeRentalsExist;

            await _vehicleRepository.UpdateAsync(vehicleCopyId, vehicle);

            if (!string.IsNullOrEmpty(returnVehicleVM.Review))
            {
                var rating = new Rating
                {
                    Value = returnVehicleVM.Rating ?? 0,
                    Comment = returnVehicleVM.Review,
                };
            }
        }

    }
}

