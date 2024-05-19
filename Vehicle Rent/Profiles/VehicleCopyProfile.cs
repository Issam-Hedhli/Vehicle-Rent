using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.Profiles
{
    public class VehicleCopyProfile : Profile
    {
        public VehicleCopyProfile()
        {
            CreateMap<VehicleCopy, VehicleCopyReadVM>()
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => AverageRatingValue(src.RentalItems.ToList())))
                .ForMember(dest=>dest.RentalItems,opt=>opt.MapFrom(src=>src.RentalItems.ToList()))
                .ForMember(dest=>dest.Unavailabilities,opt=>opt.MapFrom(src=>src.Unavailabilities.ToList()));
        }


        private int? AverageRatingValue(List<RentalItem> rentalItems)
        {
            try
            {
                var ratings = rentalItems.Where(ri => ri.Ratings != null).Select(ri => ri.Ratings);
                var values = ratings.Where(r => r.Value != null).Select(r=>r.Value).ToList();
                var averagerating = values.Average();
                return (int)averagerating;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
    }
}
