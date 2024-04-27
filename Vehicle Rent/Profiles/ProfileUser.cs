using AutoMapper;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.Profile;

namespace Vehicle_Rent.Profiles
{
    public class ProfileUser : Profile
    {
        public ProfileUser()
        {
            CreateMap<User, ProfileDetailVM>()
                .ForMember(dest => dest.numberOfvehicleCopiesInposession, opt => opt.MapFrom(src => src.Rentals.Count(r => r.StatusId == "1")))
                .ForMember(dest => dest.numberOfRentedVehicleCopies, opt => opt.MapFrom(src => src.Rentals.Count(r => r.StatusId == "2")))
                .ForMember(dest => dest.VehicleImages, opt => opt.MapFrom(src => src.Rentals
                    .Select(r => r.VehicleCopy.Vehicle.Photo)
                    .Distinct()));
        }

    }
}
