using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.Services.VehicleRent;
using Vehicle_Rent.ViewModels.ReturnVehicle;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class ReturnVehicleController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IVehicleCatalogueService _vehicleCatalogueService;
        private readonly IMapper _mapper;

        public ReturnVehicleController(IRentalService rentalService, IVehicleCatalogueService vehicleCatalogueService, IMapper mapper)
        {
            _rentalService = rentalService;
            _vehicleCatalogueService = vehicleCatalogueService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ReturnVehicle(string vehicleId)
        {
            var vehicle = await _vehicleCatalogueService.GetVehicleByIdAsync(vehicleId);
            var vehicleDetailVM = _mapper.Map<VehicleDetailVM>(vehicle);
            ReturnVehicleVM returnVehicleVM = new ReturnVehicleVM() { VehicleDetailVM = vehicleDetailVM, Confirmation = false };
            HttpContext.Session.SetString("VehicleId", vehicleId );
            return View(returnVehicleVM);
        }


        [HttpPost]
        public async Task<IActionResult> Returnvehicle(ReturnVehicleVM returnVehicleVM)
        {
            var vehicleId = HttpContext.Session.GetString("VehicleId");
            var vehicle = await _vehicleCatalogueService.GetVehicleByIdAsync(vehicleId);
            var vehicleVm = _mapper.Map<VehicleDetailVM>(vehicle);
            returnVehicleVM.VehicleDetailVM = vehicleVm;
            if (!ModelState.IsValid)
            {
                return View(returnVehicleVM);
            }
            else
            {
                string Id = User.FindFirstValue("Id");
                if (returnVehicleVM.Confirmation)
                {
                    await _rentalService.ReturnVehicleCopy(returnVehicleVM, Id);
                    return RedirectToAction("RentedVehicles", "VehicleCatalogue");
                }
                else
                {
                    return View(returnVehicleVM);
                }
            }

        }
    }
}
