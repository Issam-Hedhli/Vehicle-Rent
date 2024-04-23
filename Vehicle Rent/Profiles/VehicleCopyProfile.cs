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
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => AverageRating(src.RentalItems)))
                .ForMember(dest=>dest.RentalItems,opt=>opt.MapFrom(src=>src.RentalItems.Where(ri=>ri.StatusId=="2").ToList()))
                .ForMember(dest=>dest.Unavailabilities,opt=>opt.MapFrom(src=>src.Unavailabilities.ToList()));
        }

        private object AverageRating(ICollection<RentalItem> rentalItems)
        {
            
            var ratings = rentalItems.Select(ri => ri.Ratings).Where(r=>r!=null).Select(r=>r.Value).Where(v=>v.Value!=null).ToList();
            if (ratings.IsNullOrEmpty() )
            {
                return null;
            }
            return (int)ratings.Average();
        }
    }
}
