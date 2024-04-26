using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.Services.VehicleRent;
using Vehicle_Rent.ViewModels.ReturnVehicle;
using Vehicle_Rent.ViewModels.VehicleCopyVM;
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
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ReturnVehicleCopy(string vehicleCopyId)
        {
            var vehicleCopy = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(vehicleCopyId);
            var vehicleCopyVM = _mapper.Map<VehicleCopyReadVM>(vehicleCopy);
            ReturnVehicleVM returnVehicleVM = new ReturnVehicleVM()
            {
                VehicleCopyReadVM=vehicleCopyVM,
                Confirmation = false
            };
            HttpContext.Session.SetString("VehicleCopyId", vehicleCopyId );
            return View(returnVehicleVM);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> ReturnVehicleCopy(ReturnVehicleVM returnVehicleVM)
        {
            var vehicleCopyId = HttpContext.Session.GetString("VehicleCopyId");
            var vehicleCopy = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(vehicleCopyId);
            var vehicleCopyVm = _mapper.Map<VehicleCopyReadVM>(vehicleCopy);
            var Id = User.FindFirstValue("Id");
            if (!string.IsNullOrEmpty(Id))
            {
                //WasAlreadyRented
                var rentalItems = vehicleCopy.RentalItems;
                var previousRentalItemsByUser = rentalItems
                    .Where(ri => ri.UserId == Id)
                    .Where(ri => ri.StatusId == "2")
                    .ToList();
                vehicleCopyVm.WasAlreadyRented = previousRentalItemsByUser.Any();
                //IsBeingRented
                var actualRentalItemsByUser = rentalItems
                    .Where(ri => ri.UserId == Id)
                    .Where(ri => ri.StatusId == "1")
                    .ToList();
                vehicleCopyVm.IsBeingRented = actualRentalItemsByUser.Any();
            }
            returnVehicleVM.VehicleCopyReadVM = vehicleCopyVm;
            if (!ModelState.IsValid)
            {
                return View(returnVehicleVM);
            }
            else
            {
                if (returnVehicleVM.Confirmation)
                {
                    await _rentalService.ReturnVehicleCopy(returnVehicleVM, Id);
                    return RedirectToAction("RentedVehicleCopies", "VehicleCatalogue");
                }
                else
                {
                    return View(returnVehicleVM);
                }
            }

        }
    }
}
