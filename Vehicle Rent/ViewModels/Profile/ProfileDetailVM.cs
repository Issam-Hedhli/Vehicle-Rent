using Vehicle_Rent.Models;

namespace Vehicle_Rent.ViewModels.Profile
{
    public class ProfileDetailVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public int numberOfvehicleCopiesInposession { get; set; }
        public DateTime JoinedOn { get; set; } = DateTime.Now;
        public int numberOfRentedVehicleCopies { get; set; }

    }
}