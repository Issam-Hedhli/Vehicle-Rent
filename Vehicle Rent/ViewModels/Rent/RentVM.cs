using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.ViewModels.Rent
{
    public class RentVM
    {
        public VehicleCopyReadVM vehicleCopyReadVM { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
