using Vehicle_Rent.Models;

namespace Vehicle_Rent.ViewModels.VehicleCopyVM
{
    public class VehicleCopyReadVM
    {
        public string Id { get; set; }
        public int RentalPrice { get; set; }
        public Vehicle Vehicle { get; set; }
        public int? AverageRating { get; set; }
        public bool WasAlreadyRented { get; set; }
        public bool IsBeingRented { get; set; }
        public List<RentalItem> RentalItems { get; set; } = new List<RentalItem>();
        public List<Unavailability> Unavailabilities { get; set; } = new List<Unavailability>();
        public int Year { get; set; }
        public int Mileage { get; set; }
    }
}
