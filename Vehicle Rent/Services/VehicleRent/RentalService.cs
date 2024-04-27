
using Stripe;
using Stripe.Terminal;
using System.Security.Policy;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.Services.EmailSender;
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
        private readonly IUnavailabilityRepository _unavailabilityRepository;
        private readonly IEmailSender _emailSender;
        public RentalService(IVehicleRepository vehicleRepository, IRentalItemRepository rentalItemRepository, IUserRepository userRepository, IVehicleCopyRepository vehicleCopyRepository, IAvailabilityStatusRepository availabilityStatusRepository, IUnavailabilityRepository unavailabilityRepository, IEmailSender emailSender)
        {
            _vehicleRepository = vehicleRepository;
            _rentalItemRepository = rentalItemRepository;
            _userRepository = userRepository;
            _vehicleCopyRepository = vehicleCopyRepository;
            _availabilityStatusRepository = availabilityStatusRepository;
            _unavailabilityRepository = unavailabilityRepository;
            _emailSender = emailSender;
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
            await _vehicleRepository.UpdateAsync(vehicleCopy.IdVehicle, vehicle);
            //update availability
            await UpdateVehicleCopy(vehicleCopyId);

        }

        public async Task ReturnVehicleCopy(ReturnVehicleVM returnVehicleVM, string userId)
        {
            if (returnVehicleVM == null)
                throw new ArgumentNullException(nameof(returnVehicleVM));

            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            // Retrieve the vehicle copy based on the information provided in ReturnVehicleVM
            var vehicleCopyId = returnVehicleVM.VehicleCopyReadVM.Id;
            var vehicleCopy = await _vehicleCopyRepository.GetVehicleCopyByIdAsync(vehicleCopyId);

            if (vehicleCopy == null)
                throw new InvalidOperationException($"Vehicle copy with ID '{vehicleCopyId}' not found.");

            var rentalItem = vehicleCopy.RentalItems.FirstOrDefault(ri => ri.UserId == userId && ri.StatusId == "1"); 

            if (rentalItem == null)
            {
                throw new InvalidOperationException("No active rental found for the specified vehicle copy and user.");
            }

            rentalItem.EndDate = DateTime.Today;
            rentalItem.Ratings = new Rating()
            {
                Value = returnVehicleVM.Rating,
                Comment = returnVehicleVM.Review
            };
            rentalItem.Status = await _availabilityStatusRepository.GetByIdAsync("2");

            await _rentalItemRepository.UpdateAsync(rentalItem.Id, rentalItem);

            //fazet el unavailability
            await UpdateVehicleCopy(vehicleCopyId);




            //var vehicleId = vehicleCopy.IdVehicle;
            //var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId);

            //var activeRentalsExist = vehicle.VehicleCopies.Any(vc => vc.Id != vehicleCopyId && vc.RentalItems.Any(ri => ri.StatusId == "1"));

            //vehicle.IsAvailable = !activeRentalsExist;

            //await _vehicleRepository.UpdateAsync(vehicleId, vehicle);

            if (!string.IsNullOrEmpty(returnVehicleVM.Review))
            {
                var rating = new Rating
                {
                    Value = returnVehicleVM.Rating,
                    Comment = returnVehicleVM.Review
                };
            }
        }

        private async Task UpdateVehicleCopy(string vehicleCopyId)
        {
            var vehiclecopy = await _vehicleCopyRepository.GetVehicleCopyByIdAsync(vehicleCopyId);
            List<(DateTime, DateTime)> periods = vehiclecopy.RentalItems.Where(ri=>ri.StatusId=="1").Select(item => (item.StartDate, item.EndDate)).ToList();
            // Merge overlapping periods
            List<(DateTime, DateTime)> mergedPeriods = MergePeriods(periods);

            // Persist merged periods into the database
            await PersistMergedPeriods(mergedPeriods, vehicleCopyId);
        }

        private async Task PersistMergedPeriods(List<(DateTime, DateTime)> mergedPeriods, string vehicleCopyId)
        {
            var vehicleCopy = await _vehicleCopyRepository.GetVehicleCopyByIdAsync(vehicleCopyId);
            foreach (Unavailability unavailability in vehicleCopy.Unavailabilities)
            {
                await _unavailabilityRepository.DeleteAsync(unavailability.Id);

            }
            if (mergedPeriods.Count > 0)
            {
                foreach (var unav in mergedPeriods)
                {
                    var unavailability = new Unavailability() { startDate = unav.Item1, endDate = unav.Item2, VehicleCopy = vehicleCopy };
                    await _unavailabilityRepository.AddAsync(unavailability);
                }
            }
        }

        private List<(DateTime, DateTime)> MergePeriods(List<(DateTime, DateTime)> periods)
        {
            if (periods.Count == 0)
            {
                return periods;
            }
            var sortedPeriods = periods.OrderBy(p => p.Item1).ToList();
            var mergedPeriods = new List<(DateTime, DateTime)>();

            var currentPeriod = sortedPeriods[0];
            foreach (var period in sortedPeriods.Skip(1))
            {
                if (period.Item1 <= currentPeriod.Item2)
                {
                    // Merge overlapping periods
                    currentPeriod = (currentPeriod.Item1, period.Item2 > currentPeriod.Item2 ? period.Item2 : currentPeriod.Item2);
                }
                else
                {
                    // Add non-overlapping periods
                    mergedPeriods.Add(currentPeriod);
                    currentPeriod = period;
                }
            }
            mergedPeriods.Add(currentPeriod); // Add the last period
            return mergedPeriods;
        }
    }
}

