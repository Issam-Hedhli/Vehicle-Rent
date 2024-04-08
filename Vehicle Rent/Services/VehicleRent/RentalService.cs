
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;

namespace Vehicle_Rent.Services.VehicleRent
{
	public class RentalService : IRentalService
	{
		private readonly IVehicleRepository _vehicleRepository;
		private readonly IRentalItemRepository _rentalItemRepository;
		private readonly IUserRepository _userRepository;
		public RentalService(IVehicleRepository vehicleRepository, IRentalItemRepository rentalItemRepository, IUserRepository userRepository)
		{
			_vehicleRepository = vehicleRepository;
			_rentalItemRepository = rentalItemRepository;
			_userRepository = userRepository;
		}
		public async Task<RentalItem> RentVehicle(string vehicleCopyId, string userId)
		{
			var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleCopyId);
			if (vehicle == null || vehicle.IsAvailable)
			{
				return null;
			}

			var rental = new RentalItem 
			{
				VehicleCopyId = vehicleCopyId,
				UserId = userId,
				StartDate = DateTime.Now
			};

			//await _rentalItemRepository.AddRentalAsync(rental); 
			//vehicle.IsAvailable = false; 
			//await _vehicleRepository.UpdateVehicleAsync(vehicle); 

			return null;
		}

		public async Task ReturnVehicle(string rentalId)
		{
			//var rental = await _rentalItemRepository.GetRentalByIdAsync(rentalId); 
			//if (rental == null)
			//{
			//	return; 
			//}

			//rental.RentalEnd = DateTime.Now;
			//await _rentalRepository.UpdateRentalAsync(rental); 

		}

	}
}

