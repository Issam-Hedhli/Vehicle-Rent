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
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => AverageRatingValue(src.RentalItems.Where(ri=>ri.StatusId=="2").ToList())))
                .ForMember(dest=>dest.RentalItems,opt=>opt.MapFrom(src=>src.RentalItems.Where(ri=>ri.StatusId=="2").ToList()))
                .ForMember(dest=>dest.Unavailabilities,opt=>opt.MapFrom(src=>src.Unavailabilities.ToList()));
        }


        private int AverageRatingValue(List<RentalItem> rentalItems)
        {
            if (rentalItems == null || !rentalItems.Any())
                return 0;

            var ratings = rentalItems.Select(r => r.Ratings?.Value)
                                     .Where(r => r != null)
                                     .ToList();

            if (!ratings.Any())
                return 0;

            try
            {
                return (int)ratings.Average();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
