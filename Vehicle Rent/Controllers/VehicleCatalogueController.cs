using Microsoft.AspNetCore.Mvc;
using Vehicle_Rent.Repository.Specific;
using Vehicle_Rent.Services.VehicleCatalogue;

namespace Vehicle_Rent.Controllers
{
	public class VehicleCatalogueController : Controller
	{
		private IVehicleCatalogueService _vehicleCatalogueService;
		private IVehicleRepository _vehicleRepository; 

		public VehicleCatalogueController (IVehicleCatalogueService vehicleCatalogueService , IVehicleRepository vehicleRepository)
		{
			_vehicleCatalogueService = vehicleCatalogueService;
			_vehicleRepository = vehicleRepository;

		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Issam()
		{
			return View("CarListing");
		}
	}
}
