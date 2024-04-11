using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class VehicleCatalogueController : Controller
	{
		private IVehicleCatalogueService _vehicleCatalogueService;
		private IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleCatalogueController> _logger;

		public VehicleCatalogueController (IVehicleCatalogueService vehicleCatalogueService , IVehicleRepository vehicleRepository, IMapper mapper, ILogger<VehicleCatalogueController> logger)
		{
			_vehicleCatalogueService = vehicleCatalogueService;
			_vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _logger = logger;

		}
		public async Task<IActionResult> Vehicle(string VehicleId)
		{
            _logger.LogInformation("Received VehicleId: {VehicleId}", VehicleId);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid VehicleId: {VehicleId}", VehicleId);

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

        public IActionResult Vehicles()
        {
            return View("vehicles");
        }
    }
}
