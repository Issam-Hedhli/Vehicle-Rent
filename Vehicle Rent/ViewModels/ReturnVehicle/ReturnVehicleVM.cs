using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.ViewModels.ReturnVehicle
{
    public class ReturnVehicleVM
    {
        public VehicleDetailVM? VehicleDetailVM { get; set; }
        public string? Review { get; set; }
        public int? Rating { get; set; }
        public bool Confirmation { get; set; }
    }
}
