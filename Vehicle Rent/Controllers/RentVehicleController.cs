using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vehicle_Rent.Models;
using Vehicle_Rent.Services.EmailSender;
using Vehicle_Rent.Services.Payment;
using Vehicle_Rent.Services.Profile;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.Services.VehicleRent;
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
        private readonly IEmailSender _emailSender;
        private readonly IRentalService _rentalService;
        private readonly IProfileService _profileService;

        public RentVehicleController(IMapper mapper, IVehicleCatalogueService vehicleCatalogueService, IPaymentService paymentService, IEmailSender emailSender, IRentalService rentalService, IProfileService profileService)
        {
            _mapper = mapper;
            _vehicleCatalogueService = vehicleCatalogueService;
            _paymentService = paymentService;
            _emailSender = emailSender;
            _rentalService = rentalService;
            _profileService = profileService;
        }


        public async Task<IActionResult> RentVehicleCopy (string vehicleCopyId)
        {
            // review men houni
            var vehicleCopy = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(vehicleCopyId);
            var vehicleCopyReadVM = _mapper.Map<VehicleCopyReadVM>(vehicleCopy);
            var rentVM = new RentVM(); 
            HttpContext.Session.SetString("vehicleCopyId", vehicleCopyId);
            return View(rentVM);
        }
        [HttpPost]
        public async Task<IActionResult> RentVehicleCopy (RentVM rentVM)
        {
            var vehicleCopyId = HttpContext.Session.GetString("vehicleCopyId");
            var vehicleCopy = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(vehicleCopyId);
            rentVM.vehicleCopyReadVM=_mapper.Map<VehicleCopyReadVM>(vehicleCopy);
            if (!ModelState.IsValid)
            {
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
            var cancelUrl = Url.Action("StepUpRentVehicle", "RentVehicle", rentVM, Request.Scheme);
            var successUrl = Url.Action("Index","Home", null,Request.Scheme);
            var currency = "usd";
            var session = _paymentService.CreateCheckOutSession(amount.ToString(), currency, successUrl, cancelUrl);
            return Redirect(session);
        }
        public async Task<IActionResult> StepUpRentVehicle (RentVM rentVM)
        {
            var Id = User.FindFirstValue("Id");
            await _rentalService.RentVehicleCopy(rentVM.vehicleCopyReadVM.Id, Id, rentVM.startDate, rentVM.endDate);
            var callbackUrl = Url.Action("VehicleCatalogue", "RentedVehicleCopies", null, Request.Scheme);
            var customer = await _profileService.GetCustomerByIdAsync(Id);
            await _emailSender.SendEmailAsync(
                //email
                customer.Email,
                //subject
                "Confirmation of Rental",
                //message
                "Here is a mail confirming the rental of vehicle copy "
                + rentVM.vehicleCopyReadVM.Id 
                + "between the date " 
                + rentVM.startDate.ToString("yyyy-MM-dd") 
                + " and " 
                + rentVM.endDate.ToString("yyyy-MM-dd")
                + " "
                + $" < a href =\"{callbackUrl}\">Rented Vechiles</a>.");

            return RedirectToAction("RentedVehicleCopies", "VehicleCatalogue");
        }
    }
}
