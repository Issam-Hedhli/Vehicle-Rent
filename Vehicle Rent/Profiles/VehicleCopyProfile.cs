using AutoMapper;
using Vehicle_Rent.Models;
using Vehicle_Rent.ViewModels.VehicleCopyVM;

namespace Vehicle_Rent.Profiles
{
    public class VehicleCopyProfile : Profile
    {
        public VehicleCopyProfile()
        {
            CreateMap<VehicleCopy, VehicleCopyReadVM>();
            CreateMap<VehicleCopy, VehicleCopyDetailVM>();
        }
    }
}
