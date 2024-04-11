using Microsoft.AspNetCore.Mvc;

namespace Vehicle_Rent.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View("VAboutUs");
        }

    }
}
