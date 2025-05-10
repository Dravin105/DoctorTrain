using DoctorTrain.Business_Layer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers.Home_Controller
{
    public class HomesController : Controller
    {
        private readonly IDoctorService _doctorService;
        public HomesController(IDoctorService doctorService)
        {
            _doctorService= doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _doctorService.GetDoctorByIdAsync(id);
            return View(hospital);
        }
    }
}
