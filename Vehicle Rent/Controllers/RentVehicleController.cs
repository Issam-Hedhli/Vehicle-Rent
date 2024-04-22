using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Rent.Models;
using Vehicle_Rent.Services.Payment;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.ViewModels.Rent;
using Vehicle_Rent.ViewModels.VehicleCopyVM;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class RentVehicleController:Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleCatalogueService _vehicleCatalogueService;
        private readonly IPaymentService _paymentService;

        public RentVehicleController(IMapper mapper, IVehicleCatalogueService vehicleCatalogueService, IPaymentService paymentService)
        {
            _mapper = mapper;
            _vehicleCatalogueService = vehicleCatalogueService;
            _paymentService = paymentService;
        }


        public async Task<IActionResult> RentVehicleCopy (string vehicleCopyId)
        {
            // review men houni
            var vehicle = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(vehicleCopyId);
            var vehicleReadVM = _mapper.Map<VehicleDetailVM>(vehicle);
            var rentVM = new RentVM(); 
            HttpContext.Session.SetString("vehicleCopyId", vehicleCopyId);
            return View(rentVM);
        }
        [HttpPost]
        public async Task<IActionResult> RentVehicleCopy (RentVM rentVM)
        {
            if (!ModelState.IsValid)
            {
                var vehicleCopyId = HttpContext.Session.GetString("vehicleCopyId");
                var vehicleCopy = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(vehicleCopyId);
                rentVM.vehicleCopyReadVM = _mapper.Map<VehicleCopyReadVM>(vehicleCopy);
                return View(rentVM);
            }
            //nchouf el availability mtaa lvehicle copy
            //if (rentVM.startDate>rentVM.vehicleCopyReadVM.UnavailabilityStart || rentVM.endDate<rentVM.vehicleCopyReadVM.UnavailabilityEnd)
            //{
            //    return View(rentVM);
            //}
            //n3addih lecheckout
            var duration = (rentVM.endDate - rentVM.startDate).Days;
            var amount = duration * rentVM.vehicleCopyReadVM.RentalPrice;
            var successUrl = Url.Action("RentHistory", "VehicleCatalogue", rentVM, Request.Scheme);
            var cancelUrl = Url.Action("Index","Home", new {message = "You cancelled you rental"},Request.Scheme);
            var currency = "usd";
            var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl);
            return Redirect(session);
        }
    }
}
