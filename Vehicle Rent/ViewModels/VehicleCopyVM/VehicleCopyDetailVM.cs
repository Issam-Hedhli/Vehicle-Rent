
using Vehicle_Rent.Models;

namespace Vehicle_Rent.ViewModels.VehicleCopyVM
{
    public class VehicleCopyDetailVM
    {
        public string Id { get; set; }
        public int RentalPrice { get; set; }
        public string IdVehicle { get; set; }
        public List<Rating> Ratings { get; set; }
        public bool WasAlreadyRented { get; set; }
        public bool IsBeingRented { get; set; }
    }
}
