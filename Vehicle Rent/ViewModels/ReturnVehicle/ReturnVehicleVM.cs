using System.ComponentModel.DataAnnotations;
using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.ViewModels.ReturnVehicle
{
    public class ReturnVehicleVM
    {
        public VehicleCopyReadVM? VehicleCopyReadVM { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        [MustBeTrue(ErrorMessage = "You must confirm the return.")]
        public bool Confirmation { get; set; }
    }
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public MustBeTrueAttribute()
        {
            ErrorMessage = "The {0} field must be true.";
        }

        public override bool IsValid(object value)
        {
            return value is bool && (bool)value == true;
        }
    }
}
