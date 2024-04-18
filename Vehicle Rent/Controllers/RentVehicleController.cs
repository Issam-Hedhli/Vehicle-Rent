using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vehicle_Rent.Services.VehicleCatalogue;
using Vehicle_Rent.Services.VehicleCopyStore;
using Vehicle_Rent.ViewModels.Rent;
using Vehicle_Rent.ViewModels.VehicleVM;

namespace Vehicle_Rent.Controllers
{
    public class RentVehicleController:Controller
    {
        private readonly IVehicleCopyStoreService _vehicleCopyStoreService;
        private readonly IMapper _mapper;


        public RentVehicleController(IVehicleCopyStoreService vehicleCopyStoreService, IMapper mapper)
        {
            _vehicleCopyStoreService = vehicleCopyStoreService;
            _mapper = mapper;
        }


        public async Task<IActionResult> RentVehicleCopy (string vehicleCopyId)
        {
            // review men houni
            var vehicle = await _vehicleCopyStoreService.GetVehicleCopyByIdAsync(vehicleCopyId);
            var vehicleReadVM = _mapper.Map<VehicleDetailVM>(vehicle);
            var rentVM = new RentVM(); ////specifics
            HttpContext.Session.SetString("vehicleCopyId", vehicleCopyId);
            return View(rentVM);
        }
        [HttpPost]
        public async Task<IActionResult> RentVehicleCopy(RentVM rentVM)
        {
            return View();
        }
    }
}
