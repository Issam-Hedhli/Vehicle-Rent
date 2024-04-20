using Vehicle_Rent.Models;

namespace Vehicle_Rent.ViewModels.VehicleCopyVM
{
    public class VehicleCopyReadVM
    {
        public string Id { get; set; }
        public int RentalPrice { get; set; }
        public string IdVehicle { get; set; }
        public int AverageRating { get; set; }
        public bool WasAlreadyRented { get; set; }
        public bool IsBeingRented { get; set; }
        public List<RentalItem> RentalItems { get; set; }
        public DateTime UnavailabilityStart { get; set; }
        public DateTime UnavailabilityEnd { get; set; }
    }
}
