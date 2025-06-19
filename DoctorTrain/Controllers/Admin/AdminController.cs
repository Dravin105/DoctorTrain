using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Model.Dto; // Assuming your services are in this namespace

namespace DoctorTrain.Controllers
{
    [Authorize(Roles = "Admin")]  // Only Admin can access this controller
    public class AdminController : Controller
    {
        // Private readonly fields for the services
        private readonly IDoctorService _doctorService;
        private readonly IHospitalService _hospitalService;
        private readonly IPatientService _patientService;
        private readonly IContactMessageService _contactService;

        // Constructor where the services are injected
        public AdminController(IDoctorService doctorService, IHospitalService hospitalService, IPatientService patientService, IContactMessageService contactService)
        {
            _doctorService = doctorService;
            _hospitalService = hospitalService;
            _patientService = patientService;
            _contactService = contactService;
        }

        // Dashboard page for Admin
        public IActionResult Index()
        {
            // Admin Dashboard ki view ko return karo
            return View();
        }
        [HttpGet]

        // Method to manage doctors
        public async Task<IActionResult> ManageDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();  // ✅ await added
            return View(doctors);
        }

        //public async Task<IActionResult> DoctorAllLIst()
        //{
        //    var doctors = await _doctorService.GetAllDoctorsAsync();
        //    return View(doctors);
        //}

        public IActionResult DoctorCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DoctorCreate(DoctorDto doctorDto)
        {
            if (!ModelState.IsValid)
                return View(doctorDto);

            await _doctorService.AddDoctorAsync(doctorDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DoctorEdit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        //// POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoctorEdit(int id, DoctorDto doctorDto)
        {
            if (id != doctorDto.Id) return NotFound();

            if (!ModelState.IsValid) return View(doctorDto);

            try
            {
                await _doctorService.UpdateDoctorAsync(id, doctorDto);
            }
            catch
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                if (doctor == null) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }


        //// GET: Doctor/Delete/5
        public async Task<IActionResult> DoctorDelete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        //// POST: Doctor/Delete/5
        [HttpPost, ActionName("DoctorDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoctorDeleteConfirmed(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DoctorDetails(int id)
        {
            var hospital = await _doctorService.GetDoctorByIdAsync(id);
            return View(hospital);
        }
        //// Method to manage hospitals
        [HttpGet]
        public async Task<IActionResult> ManageHospitals()
        {
            var hospitals = await _hospitalService.GetAllAsync();  // ✅ await added
            return View(hospitals);
        }

        public async Task<IActionResult> HospitalsDetails(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            return View(hospital);
        }

        public IActionResult HospitalsCreate() => View();

        [HttpPost]
        public async Task<IActionResult> HospitalsCreate(HospitalDto hospitalDto)
        {
            if (ModelState.IsValid)
            {
                await _hospitalService.CreateAsync(hospitalDto);
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalDto);
        }

        public async Task<IActionResult> HospitalsEdit(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> HospitalsEdit(int id, HospitalDto hospitalDto)
        {
            if (ModelState.IsValid)
            {
                await _hospitalService.UpdateAsync(id, hospitalDto);
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalDto);
        }

        public async Task<IActionResult> HospitalsDelete(int id)
        {
            var hospital = await _hospitalService.GetByIdAsync(id);
            return View(hospital);
        }

        [HttpPost, ActionName("HospitalsDelete")]
        public async Task<IActionResult> HospitalsDeleteConfirmed(int id)
        {
            await _hospitalService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        //// Method to manage Patients
        public async Task<IActionResult> ManagePatients()
        {
            var patient = await _patientService.GetAllPatientsAsync();
            return View(patient);
        }
        public async Task<IActionResult> PatientDelete(int id)
        {
            var doctor = await _patientService.GetPatientByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("PatientDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientDeleteConfirmed(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PatientDetails(int id)
        {
            var hospital = await _patientService.GetPatientByIdAsync(id);
            return View(hospital);
        }
        //
        public async Task<IActionResult> Messages()
        {
            var allMessages = await _contactService.GetAllAsync();
            return View(allMessages);
        }

        public async Task<IActionResult> UnreadCount()
        {
            var unreadMessages = await _contactService.GetUnreadAsync();

            foreach (var msg in unreadMessages)
            {
                await _contactService.MarkAsReadAsync(msg.Id); // sab message read mark karo
            }

            var allMessages = await _contactService.GetAllAsync();
            return View(allMessages);
        }

    }


    // Additional methods as per your requirements
}
