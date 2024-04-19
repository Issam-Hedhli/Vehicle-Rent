using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.VehicleRent
{
	public interface IRentalService
	{
		public Task RentVehicleCopy(string vehicleCopyId, string userId, DateTime startDate, DateTime endDate);
		public Task ReturnVehicleCopy(string rentalId, string userId);
	}
}
