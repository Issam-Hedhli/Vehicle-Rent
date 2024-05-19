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
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
            .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.VModel.Name));
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
    }
}