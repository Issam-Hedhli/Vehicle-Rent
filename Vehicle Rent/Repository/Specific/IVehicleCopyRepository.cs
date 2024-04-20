using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

namespace Vehicle_Rent.Repository.Specific
{
    public interface IVehicleCopyRepository : IEntityBaseRepository<VehicleCopy>
    {
        //public Task<VehicleCopy> GetEagerVehicleCopyById(string id);
        Task<List<VehicleCopy>> GetVehiclesCopiesByVehicleCopy(string vehicleId);
    }
}
