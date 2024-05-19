using System.ComponentModel.DataAnnotations;
using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.ViewModels.Rent
{
    public class RentVM
    {
        public VehicleCopyReadVM? vehicleCopyReadVM { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The Start Date field is required.")]
        [FutureDate(ErrorMessage = "Start date must be today or in the future.")]
        public DateTime startDate { get; set; } = DateTime.Today.AddDays(1);

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "The End Date field is required.")]
        [DateGreaterThan(nameof(startDate), ErrorMessage = "End Date must be greater than Start Date.")]
        public DateTime endDate { get; set; } =DateTime.Today.AddDays(1);
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime startDate = (DateTime)value;
                if (startDate.Date < DateTime.Today.Date)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}



