using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.ViewModels.Profile
{
    public class ProfileDetailVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public List<VehicleReadVM>Images { get; set; }
        public int numberOfvehiclesInposession { get; set; }
    }
}
