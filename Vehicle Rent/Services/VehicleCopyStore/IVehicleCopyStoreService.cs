using Vehicle_Rent.Models;

namespace Vehicle_Rent.Services.VehicleCopyStore
{
    public interface IVehicleCopyStoreService
    {
        public Task<VehicleCopy> GetVehicleCopyByIdAsync(string vehicleId);
    }
}
