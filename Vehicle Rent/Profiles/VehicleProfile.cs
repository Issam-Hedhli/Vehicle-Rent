using AutoMapper;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleReadVM>()
            .ForMember(dest => dest.NumberOfAvailableVehicles, opt => opt.MapFrom(src => AvailableVehicles(src)))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => AverageRating(src)))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.VModel.Name))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src =>
                src.VehicleCopies
                    .SelectMany(ri => ri.RentalItems
                        .Select(r => r.Ratings))
                    .Where(rating => rating != null)
                    .ToList()));
        }
        private int AvailableVehicles(Vehicle Vehicle)
        {
            var num = 0;
            foreach (var VehicleCopy in Vehicle.VehicleCopies)
            {
                bool testAvailable = !VehicleCopy.RentalItems.Select(bi => bi.StatusId).Any(si => si == "1");
                if (testAvailable)
                {
                    num++;
                }
            }
            return num;
        }
        public int AverageRating(Vehicle Vehicle)
        {
            // Check if the Vehicle has any copies with  ratings
            if (Vehicle.VehicleCopies.Any(bc => bc.RentalItems.Any(bi => bi.Ratings != null)))
            {
                // Flatten the  ratings of all Vehicle copies
                var allRatings = Vehicle.VehicleCopies
                    .SelectMany(bc => bc.RentalItems)
                    .Where(bi => bi.Ratings != null)
                    .Select(bi => bi.Ratings.Value)
                    .ToList();

                // Calculate the average rating if there are  ratings available
                if (allRatings.Any())
                {
                    var averageRating = allRatings.Average();
                    return Convert.ToInt32(averageRating);
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                return 5;
            }
        }
    }
}