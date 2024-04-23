using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.ViewModels
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (value == null || !(value is DateTime) || !(comparisonValue is DateTime))
            {
                return ValidationResult.Success; // Not a DateTime, so should be validated elsewhere
            }

            var date = (DateTime)value;
            var comparisonDate = (DateTime)comparisonValue;

            if (date <= comparisonDate)
            {
                return new ValidationResult(ErrorMessage ?? $"Date must be greater than {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }

    }
}
