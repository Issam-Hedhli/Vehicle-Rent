using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Generic;

public class AvailibilityStatusWrapper : IEntityBase
{
    public int Id { get; set; }
    public AvailibilityStatus Status { get; set; }
}
