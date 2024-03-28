namespace Vehicle_Rent.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public string DriverLicence { get; set; }

        public string RentalHistory { get; set; }
    }
}
