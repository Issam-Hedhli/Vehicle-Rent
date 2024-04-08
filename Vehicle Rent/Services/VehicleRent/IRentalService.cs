using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.VehicleRent
{
	public interface IRentalService
	{
		public Task<RentalItem> RentVehicle(string vehicleCopyId, string userId);
		public Task ReturnVehicle(string rentalId);
	}
}
