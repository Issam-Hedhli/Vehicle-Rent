using AutoMapper;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.Profile;

namespace Vehicle_Rent.Profiles
{
    public class ProfileUser : Profile
    {
        public ProfileUser() 
        {
            CreateMap<User, ProfileDetailVM>();
        }
    }
}
