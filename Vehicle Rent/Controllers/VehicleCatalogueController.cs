using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Vehicle_Rent.Models;
using Vehicle_Rent.Services.EmailSender;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.ViewModels.VehicleCopyVM;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class VehicleCatalogueController : Controller
	{
		private IVehicleCatalogueService _vehicleCatalogueService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

		public VehicleCatalogueController (IVehicleCatalogueService vehicleCatalogueService , IMapper mapper , IEmailSender emailSender)
		{
			_vehicleCatalogueService = vehicleCatalogueService;
            _mapper = mapper;
            _emailSender = emailSender;

		}
        public async Task<IActionResult> Vehicles(string searchString, string company, string model, int rentalPrice , int minPrice , int maxPrice)
        {
            //await _emailSender.SendEmailAsync("Customer@carrental.com", "testsubject", "testmessage");
            string id = User.FindFirstValue("Id");
            var vehicles = await _vehicleCatalogueService.GetAllVehiclesAsync();
            var vehicledetailVms = _mapper.Map<List<VehicleDetailVM>>(vehicles);
            vehicledetailVms = Filter(vehicledetailVms, searchString, company, model);
            ViewBag.Title = "Browse vehicles";
            ViewBag.Models = vehicledetailVms.Select(m => m.ModelName).Distinct().ToList();
            ViewBag.Companies = vehicledetailVms.Select(c => c.CompanyName).Distinct().ToList();
            //ViewBag.Redirect = "vehicles";
            return View(vehicledetailVms);
        }
        public List<VehicleDetailVM> Filter(List<VehicleDetailVM> VehicleDetailVMs, string searchString, string company, string model)
        {
            if (!searchString.IsNullOrEmpty())
            {
                VehicleDetailVMs = VehicleDetailVMs.Where(br => br.Name.Contains(searchString)).ToList();
            }
            if (!company.IsNullOrEmpty())
            {
                VehicleDetailVMs = VehicleDetailVMs.Where(c => c.CompanyName == company).ToList();
            }
            if (!model.IsNullOrEmpty())
            {
                VehicleDetailVMs = VehicleDetailVMs.Where(m => m.ModelName == model).ToList();
            }

            return VehicleDetailVMs;
        }
        public async Task<IActionResult> VehicleCopy(string VehicleCopyId)
        {
            VehicleCopy vehicleCopy = await _vehicleCatalogueService.GetVehicleCopyByIdAsync(VehicleCopyId);
            var vehicleCopyReadVM = _mapper.Map<VehicleCopyReadVM>(vehicleCopy);
            string Id = User.FindFirstValue("Id");
            if (!string.IsNullOrEmpty(Id))
            {
                //WasAlreadyRented
                var rentalItems = vehicleCopy.RentalItems;
                var previousRentalItemsByUser = rentalItems
                    .Where(ri => ri.UserId == Id)
                    .Where(ri => ri.StatusId == "2")
                    .ToList();
                vehicleCopyReadVM.WasAlreadyRented = previousRentalItemsByUser.Any();
                //IsBeingRented
                var actualRentalItemsByUser = rentalItems
                    .Where(ri => ri.UserId == Id)
                    .Where(ri => ri.StatusId == "1")
                    .ToList();  
                vehicleCopyReadVM.IsBeingRented = actualRentalItemsByUser.Any();
            }
            return View(vehicleCopyReadVM);
        }
        public async Task<IActionResult> Vehicle(string vehicleId, int minPrice, int maxPrice, DateTime startDate, DateTime endDate)
        {
            if (vehicleId == null)
            {
                vehicleId = HttpContext.Session.GetString("vehicleId");
            }
            List<VehicleCopy> vehicleCopies = await _vehicleCatalogueService.GetVehiclesCopiesByVehicleId(vehicleId);
            HttpContext.Session.SetString("vehicleId",vehicleId);
            List<VehicleCopyReadVM> vehicleCopyReadVms = new List<VehicleCopyReadVM>();
            string Id = User.FindFirstValue("Id");
            if (!string.IsNullOrEmpty(Id))
            {
                foreach (var vehicleCopy in vehicleCopies) 
                {
                    var vehicleCopyReadVM = _mapper.Map<VehicleCopyReadVM>(vehicleCopy);
                    //WasAlreadyRented
                    var rentalItems = vehicleCopy.RentalItems;
                    var previousRentalItemsByUser = rentalItems
                        .Where(ri => ri.UserId == Id)
                        .Where(ri => ri.StatusId == "2")
                        .ToList();
                    vehicleCopyReadVM.WasAlreadyRented = previousRentalItemsByUser.Any();
                    //IsBeingRented
                    var actualRentalItemsByUser = rentalItems
                        .Where(ri => ri.UserId == Id)
                        .Where(ri => ri.StatusId == "1")
                        .ToList();
                    vehicleCopyReadVM.IsBeingRented = actualRentalItemsByUser.Any();
                    vehicleCopyReadVms.Add(vehicleCopyReadVM);
                }
            }
            else
            {
                vehicleCopyReadVms = _mapper.Map<List<VehicleCopyReadVM>>(vehicleCopies);
            }
            var vehicle = await _vehicleCatalogueService.GetVehicleByIdAsync(vehicleId);
            var vehicleReadVM = _mapper.Map<VehicleDetailVM>(vehicle);
            vehicleCopyReadVms = Filter(vehicleCopyReadVms, minPrice, maxPrice,startDate,endDate);
            vehicleReadVM.VehicleCopyReadVMs = vehicleCopyReadVms;
            return View("Vehicle",vehicleReadVM);
        }

        public List<VehicleCopyReadVM> Filter(List<VehicleCopyReadVM> vehicleCopyReadVMs, int minRentalPrice, int maxRentalPrice, DateTime startDate, DateTime endDate)
        {
            // Filter by rental price
            if (minRentalPrice > 0)
            {
                vehicleCopyReadVMs = vehicleCopyReadVMs.Where(vc => vc.RentalPrice >= minRentalPrice).ToList();
            }
            if (maxRentalPrice > 0)
            {
                vehicleCopyReadVMs = vehicleCopyReadVMs.Where(vc => vc.RentalPrice <= maxRentalPrice).ToList();
            }

            // Filter by availability
            vehicleCopyReadVMs = vehicleCopyReadVMs.Where(vc =>
                vc.Unavailabilities.All(u => u.endDate < startDate || u.startDate > endDate)).ToList();

            return vehicleCopyReadVMs;
        }


        public async Task<IActionResult> RentedVehicles()
        {
            string Id = User.FindFirstValue("Id");
            var vehiclecopies = await _vehicleCatalogueService.GetRentedVehicleCopiesByUserIdAsync(Id);
            var vehiclecopyvms = _mapper.Map<List<VehicleCopyReadVM>>(vehiclecopies);
        
            return View("vehiclecopies", vehiclecopyvms);       }


        public async Task<IActionResult> ReturnedVehicles(string searchString, string company, string model)
        {
            string Id = User.FindFirstValue("Id");
            var vehicles = await _vehicleCatalogueService.GetReturnedVehiclesByCustomerIdAsync(Id);
            var vehicleDetailVms = new List<VehicleDetailVM>();
            if (Id == null)
            {
                vehicleDetailVms = _mapper.Map<List<VehicleDetailVM>>(vehicles);
            }
            else
            {
                foreach (Vehicle vehicle in vehicles)
                {
                    var vehicleDetailVm = _mapper.Map<VehicleDetailVM>(vehicle);
                    vehicleDetailVm.isAlreadyRented = _vehicleCatalogueService.IsAlreadyRented(vehicle, Id);
                    vehicleDetailVm.isCurrentlyrented = _vehicleCatalogueService.IsCurrentlyRented(vehicle, Id);
                    vehicleDetailVms.Add(vehicleDetailVm);
                }
            }
            vehicleDetailVms = Filter(vehicleDetailVms, searchString, company, model);
            ViewBag.Name = "Returned Vehicles";
            ViewBag.Redirect = "ReturnedVehicles";
            ViewBag.Models = vehicleDetailVms.Select(b => b.ModelName).Distinct().ToList();
            ViewBag.Companies = vehicleDetailVms.Select(b => b.CompanyName).Distinct().ToList();
            ViewBag.ModelValue = model;
            ViewBag.CompanyValue = company;
            return View("Vehicles", vehicleDetailVms);
        }
    }
}
