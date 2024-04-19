using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;

namespace Vehicle_Rent.Services.VehicleCopyStore
{
    public class VehicleCopyStoreService : IVehicleCopyStoreService
    {
        private readonly IVehicleCopyRepository _vehicleCopyRepository;

        public VehicleCopyStoreService(IVehicleCopyRepository vehicleCopyRepository)
        {
            _vehicleCopyRepository = vehicleCopyRepository;
        }

        public async Task<VehicleCopy> GetVehicleCopyByIdAsync(string vehicleId)
        {
            var vehicleCopy = await _vehicleCopyRepository.GetByIdAsync(vehicleId);
            //var vehicleCopy = await _vehicleCopyRepository.GetEagerVehicleCopyById(vehicleId);
            return vehicleCopy;
        }
    }
}
