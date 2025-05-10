using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers.Admin
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
