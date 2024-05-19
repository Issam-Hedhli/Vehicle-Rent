using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.ViewModels.VehicleVM
{
    public class VehicleReadVM
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RentalPrice { get; set; }
        public string? Photo { get; set; }
        public bool IsAvailable { get; set; }
        public bool isCurrentlyrented { get; set; }
        public bool isAlreadyRented { get; set; }
        public List<Rating>? Ratings { get; set; }
        public string CompanyName { get; set; }
        public string ModelName { get; set; }
        public int NumberOfAvailableVehicles { get; set; }
        public List<VehicleCopyReadVM> VehicleCopyReadVMs { get; set; }

    }

}

