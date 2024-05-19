using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.ReturnVehicle;

namespace Vehicle_Rent.Services.VehicleRent
{
	public interface IRentalService
	{
		public Task RentVehicleCopy(string vehicleCopyId, string userId, DateTime startDate, DateTime endDate);
		public Task ReturnVehicleCopy(ReturnVehicleVM returnVehicleVM, string userId);
		public Task UpdateVehicleCopies();


    }
}
