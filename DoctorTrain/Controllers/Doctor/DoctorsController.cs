using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers.Doctor
{
    public class DoctorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
