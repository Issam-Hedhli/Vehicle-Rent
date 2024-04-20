using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Vehicle_Rent.Models;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.Services.EmailSender;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class VehicleCatalogueController : Controller
	{
		private IVehicleCatalogueService _vehicleCatalogueService;
		private IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

		public VehicleCatalogueController (IVehicleCatalogueService vehicleCatalogueService , IVehicleRepository vehicleRepository, IMapper mapper , IEmailSender emailSender)
		{
			_vehicleCatalogueService = vehicleCatalogueService;
			_vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _emailSender = emailSender;

		}
		public async Task<IActionResult> Vehicle(string VehicleId)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid VehicleId: " + string.Join(", ", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))));
            }

            var vehicleDetails = await _vehicleCatalogueService.GetVehicleByIdAsync(VehicleId);

            if (vehicleDetails == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VehicleDetailVM>(vehicleDetails);

            return View(viewModel);

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
        public async Task<IActionResult> VehicleCopy()
        {
            return View();
        }
        public async Task<IActionResult> VehicleCopies()
        {
            return View();
        }
    }
}
