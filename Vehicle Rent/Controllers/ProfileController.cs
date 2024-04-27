using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vehicle_Rent.Services.Profile;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.ViewModels.Profile;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IVehicleCatalogueService _vehicleCatalogueService;
        private readonly IMapper _mapper;
        public ProfileController(IProfileService profileService, IVehicleCatalogueService vehicleCatalogueService, IMapper mapper)
        {
            _profileService = profileService;
            _vehicleCatalogueService = vehicleCatalogueService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var Id = User.FindFirstValue("Id");
            var Customer = await _profileService.GetCustomerByIdAsync(Id);
            var profile = _mapper.Map<ProfileDetailVM>(Customer);
            var returnedvehicles = await _vehicleCatalogueService.GetReturnedVehiclesByCustomerIdAsync(Id);
            profile.Images = _mapper.Map<List<VehicleReadVM>>(returnedvehicles.Take(5).ToList());
            var currentlyBorrowedvehicles = await _vehicleCatalogueService.GetRentedVehiclesByCustomerIdAsync(Id);
            profile.numberOfvehiclesInposession = currentlyBorrowedvehicles.Count;
            ViewBag.Title = "My Profile";
            return View(profile);
        }
    }
}
