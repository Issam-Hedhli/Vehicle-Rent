using System.ComponentModel.DataAnnotations;

namespace Vehicle_Rent.ViewModels.Auth
{
    public class RegisterVM
    {
        [Required]
		public string? Name { get; set; }
		[Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The Confirmation Password field is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmationPassword { get; set; }
    }
}
