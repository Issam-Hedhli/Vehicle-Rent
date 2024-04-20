using Vehicle_Rent.Models;

namespace Vehicle_Rent.ViewModels.VehicleCopyVM
{
    public class VehicleCopyReadVM
    {
        public string Id { get; set; }
        public int RentalPrice { get; set; }
        public string VehicleId { get; set; }
        public bool WasAlreadyRented { get; set; }
        public bool IsBeingRented { get; set; }
    }
}
