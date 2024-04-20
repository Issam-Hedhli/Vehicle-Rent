using AutoMapper;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.Profiles
{
    public class VehicleCopyProfile : Profile
    {
        public VehicleCopyProfile()
        {
            CreateMap<VehicleCopy, VehicleCopyReadVM>()
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => AverageRating(src.RentalItems)));
        }

        private object AverageRating(ICollection<RentalItem> rentalItems)
        {
            var ratings = rentalItems.Select(ri => ri.Ratings.Value).ToList();
            return (int)ratings.Average();
        }
    }
}
