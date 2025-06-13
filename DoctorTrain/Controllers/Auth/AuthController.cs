using DoctorTrain.Business_Layer.Interface.IAuth;
using DoctorTrain.Model.Dto.AuthDto;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ========== LOGIN ==========
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(HospitalLoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.LoginAsync(dto);
            if (result == "InvalidEmail")
            {
                ModelState.AddModelError("Email", "Email is incorrect.");
                return View(dto);
            }
            else if (result == "InvalidPassword")
            {
                ModelState.AddModelError("Password", "Password is incorrect.");
                return View(dto);
            }
            // role ke hisab se redirect
            if (result == "Admin")
                return RedirectToAction("Index", "Admin");
            else if (result == "Doctor")
                return RedirectToAction("Index", "Doctors");
            else if (result == "Patient")
                return RedirectToAction("Index", "Patient");
            else
                return RedirectToAction("Login", "Auth"); // default
        }

        // ========== REGISTRATION OPTIONS ==========
        [HttpGet]
        public IActionResult RegisterOptions() => View();

        // ========== ADMIN REGISTRATION ==========
        [HttpGet]
        public IActionResult AdminRegistration() => View();

        [HttpPost]
        public async Task<IActionResult> AdminRegistration(AdminRegistrationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.RegisterAdminAsync(dto);
            if (result != null)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Registration failed");
            return View(dto);
        }

        // ========== DOCTOR REGISTRATION ==========
        [HttpGet]
        public IActionResult DoctorRegistration() => View();

        [HttpPost]
        public async Task<IActionResult> DoctorRegistration(DoctorRegistrationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.RegisterDoctorAsync(dto);
            if (result != null)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Invalid Admin Credentials or Registration Failed");
            return View(dto);
        }

        // ========== PATIENT REGISTRATION ==========
        [HttpGet]
        public IActionResult PatientRegistration() => View();

        [HttpPost]
        public async Task<IActionResult> PatientRegistration(PatientRegistrationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.RegisterPatientAsync(dto);
            if (result != null)
                return RedirectToAction("Login");

            ModelState.AddModelError("", "Registration Failed");
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Homes");
        }
    }
}