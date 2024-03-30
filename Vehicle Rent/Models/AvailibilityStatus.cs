using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Vehicle_Rent.Models
{
    public enum AvailibilityStatus
    {
        Available,
        Rented,
        Reserved
    }
}
