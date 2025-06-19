using DoctorTrain.Business_Layer.Interface;
using Microsoft.AspNetCore.Mvc;
namespace DoctorTrain.Controllers.Doctor
{
    public class DoctorpanalController : Controller
    {
        private readonly IDoctorService _doctorService;
       

       

        public DoctorpanalController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }
       

    }
}
