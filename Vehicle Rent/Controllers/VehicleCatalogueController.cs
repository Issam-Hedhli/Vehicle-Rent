using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;
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
        public async Task<IActionResult> Vehicles(string searchString, string company, string model, int rentalPrice , int minRentalPrice , int maxRentalPrice)
        {
            await _emailSender.SendEmailAsync("Customer@carrental.com", "testsubject", "testmessage");
            string id = User.FindFirstValue("Id");
            var vehicles = await _vehicleCatalogueService.GetAllVehiclesAsync();
            var vehicledetailVms = new List<VehicleDetailVM>();
            if(id == null)
            {
                vehicledetailVms = _mapper.Map<List<VehicleDetailVM>>(vehicles);
            }
            else
            {
                foreach (Vehicle vehicle in vehicles)
                {
                    var VehicledetailVm = _mapper.Map<VehicleDetailVM>(vehicle);
                    vehicledetailVms.Add(VehicledetailVm);
                }
            }
            vehicledetailVms = Filter(vehicledetailVms, searchString, company, model, rentalPrice , minRentalPrice , maxRentalPrice);
            ViewBag.Title = "Browse vehicles";
            ViewBag.Redirect = "vehicles";
            ViewBag.Models = vehicledetailVms.Select(m => m.ModelName).Distinct().ToList();
            ViewBag.Companies = vehicledetailVms.Select(c => c.CompanyName).Distinct().ToList();

            return View(vehicledetailVms);
        }
        public List<VehicleDetailVM> Filter(List<VehicleDetailVM> VehicleDetailVMs, string searchString, string company, string model,int rentalPrice, int minRentalPrice, int maxRentalPrice)
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
            if (minRentalPrice > 0 || maxRentalPrice > 0)
            {
                VehicleDetailVMs = VehicleDetailVMs.Where(v => v.RentalPrice >= minRentalPrice && v.RentalPrice <= maxRentalPrice).ToList();
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
        public async Task<IActionResult> Vehicle(string vehicleId)
        {
            List<VehicleCopy> vehicleCopies = await _vehicleCatalogueService.GetVehiclesCopiesByVehicleId(vehicleId);
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
            vehicleReadVM.VehicleCopyReadVMs = vehicleCopyReadVms;
            return View("Vehicle",vehicleReadVM);
        }
    }
}
