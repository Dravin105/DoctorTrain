using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Business_Layer.Service;
using DoctorTrain.Model.Dto;
using DoctorTrain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers.Home_Controller
{
    public class HomesController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IContactMessageService _contactMessageService;
        public HomesController(IDoctorService doctorService, IContactMessageService contactMessageService)
        {
            _doctorService = doctorService;
            _contactMessageService = contactMessageService;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var viewModel = new HomeViewModel
            {
                Doctors = doctors,
                ContactForm = new ContactFormDto() // blank form
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _doctorService.GetDoctorByIdAsync(id);
            return View(hospital);
        }
        public IActionResult About()
        {
            ViewData["Description"] = "DoctorTrain is India's top online doctor booking platform.";
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactFormDto dto)
        {
            if (ModelState.IsValid)
            {
                await _contactMessageService.AddMessageAsync(dto);
                TempData["Success"] = "Message sent successfully!";
                return RedirectToAction("Contact");
            }

            TempData["Error"] = "Please fix the form errors.";
            return View(dto);
        }
    }

}

