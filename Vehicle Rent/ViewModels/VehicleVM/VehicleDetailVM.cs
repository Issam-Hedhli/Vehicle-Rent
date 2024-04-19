﻿using Vehicle_Rent.Models;

namespace Vehicle_Rent.ViewModels.VehicleVM
{
    public class VehicleDetailVM
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RentalPrice { get; set; }
        public int AverageRate { get; set; }
        public string? Photo { get; set; }
        public bool IsAvailable { get; set; }
        public List<Rating>? Ratings { get; set; }
        public string CompanyName { get; set; }
        public string ModelName { get; set; }
        public int AverageRating { get; set; }
        public int NumberOfCopies { get; set; }
        public int NumberOfAvailableVehicles { get; set; }

    }

}

