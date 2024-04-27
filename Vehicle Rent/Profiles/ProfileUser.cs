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
                .ForMember(dest => dest.numberOfvehicleCopiesInposession, opt => opt.MapFrom(src => src.Rentals.Where(r => r.StatusId == "1").Count()))
                .ForMember(dest => dest.numberOfRentedVehicleCopies, opt => opt.MapFrom(src => src.Rentals.Where(r => r.StatusId == "2").Count()))
                .ForMember(dest => dest.VehicleImages, opt => opt.MapFrom(src => src.Rentals.Select(r=>r.VehicleCopy).Select(vc=>vc.Vehicle).Select(v=>v.Photo).Distinct()));
        }
    }
}
