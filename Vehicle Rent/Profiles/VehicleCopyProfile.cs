﻿using AutoMapper;
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
                .ForMember(dest=>dest.RentalItems,opt=>opt.MapFrom(src=>src.RentalItems.ToList()));
        }

        private object AverageRating(ICollection<RentalItem> rentalItems)
        {
            
            var ratings = rentalItems.Select(ri => ri.Ratings).Where(r=>r!=null).Select(r=>r.Value).ToList();
            if (ratings.IsNullOrEmpty() )
            {
                return 5;
            }
            return (int)ratings.Average();
        }
    }
}