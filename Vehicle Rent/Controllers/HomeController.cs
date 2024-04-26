using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Vehicle_Rent.Models;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.ViewModels;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleCatalogueService _vehicleCatalogueService;

        public HomeController(IMapper mapper, IVehicleCatalogueService vehicleCatalogueService)
        {
            _mapper = mapper;
            _vehicleCatalogueService = vehicleCatalogueService;
        }

        public async Task< IActionResult> Index()
        {
            var vehicles = await _vehicleCatalogueService.GetAllVehiclesAsync();
            var vehicledetailVms = _mapper.Map<List<VehicleReadVM>>(vehicles);
            ViewBag.Models = vehicledetailVms.Select(m => m.ModelName).Distinct().ToList();
            ViewBag.Companies = vehicledetailVms.Select(c => c.CompanyName).Distinct().ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
