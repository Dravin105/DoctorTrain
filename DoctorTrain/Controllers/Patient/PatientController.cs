using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Business_Layer.Service;
using DoctorTrain.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers.Patient
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly IAppointmentService _iAppointmentService;
        private readonly IPatientService _iPatientService;
        public PatientController(IAppointmentService iAppointmentService, IPatientService iPatientService)
        {
            _iAppointmentService = iAppointmentService;
            _iPatientService = iPatientService;
        } 
        [HttpGet]
        public IActionResult Index()
        {
            // Patient Dashboard ki view ko return karo
            return View();
        }
        public async Task<IActionResult> AppointmentList()
        {
            var result = await _iAppointmentService.GetAllAppointmentsAsync();
            return View(result);
        }
        public async Task<IActionResult> AppointmentCreate()
        {
            ViewBag.Doctors = await _iAppointmentService.GetDoctorsAsync();
            ViewBag.Hospitals = await _iAppointmentService.GetHospitalsAsync();
            ViewBag.Patients = await _iAppointmentService.GetPatientAsync();
             
            return View();
        }

        // POST: /Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentCreate(AppointmentReadDto dto)
        {
            if (ModelState.IsValid)
            {

                var newAppointment = await _iAppointmentService.CreateAsync(dto);
                return RedirectToAction(nameof(AppointmentConfirmation), new { id = newAppointment.Id });

            }
            return View(dto);

        }
        public async Task<IActionResult> AppointmentConfirmation(int id)
        {
            var appointment = await _iAppointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        public async Task<IActionResult> AppointmentEdit()
        {
            ViewBag.Doctors = await _iAppointmentService.GetDoctorsAsync();
            ViewBag.Hospitals = await _iAppointmentService.GetHospitalsAsync();
            ViewBag.Patients = await _iAppointmentService.GetPatientAsync();

            return View();
        }

        // POST: /Appointment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentEdit(int id, AppointmentReadDto appointmentDto) // Use AppointmentDto here
        {
            if (id != appointmentDto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _iAppointmentService.UpdateAsync(id, appointmentDto);
                return RedirectToAction(nameof(AppointmentList));
            }
            return View(appointmentDto); // Return same model type to the view
        }
        public async Task<IActionResult> AppointmentDetails(int id)
        {
            var result = await _iAppointmentService.GetAppointmentByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        public async Task<IActionResult> AppointmentDelete(int id)
        {
            var appointment = await _iAppointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);

        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("AppointmentDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _iAppointmentService.DeleteAppointmentAsync(id);
            return RedirectToAction(nameof(AppointmentList));
        }
        public async Task<IActionResult> PatientEdit(int id)
        {
            var patient = await _iPatientService.GetPatientByIdAsync(id);
            return View(patient);
        }

        // POST: /Appointment/Create
        public async Task<IActionResult> PatientProfile()
        {
            var patient = await _iPatientService.GetAllPatientsAsync();
            return View(patient);
        }

        public async Task<IActionResult> PatientCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PatientCreate(PatientDto dto)
        {
            if (ModelState.IsValid)
            {
                await _iPatientService.CreatePatientAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientEdit(int id, PatientDto patientDto)
        {
            if (ModelState.IsValid)
            {
                await _iPatientService.UpdatePatientAsync(id, patientDto);
                return RedirectToAction(nameof(PatientDetails));
            }
            return View(patientDto);
        }
        public async Task<IActionResult> PatientDelete(int id)
        {
            var doctor = await _iPatientService.GetPatientByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatientDeleteConfirmed(int id)
        {
            await _iPatientService.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PatientDetails(int id)
        {
            var hospital = await _iPatientService.GetPatientByIdAsync(id);
            return View(hospital);
        }
    }
}
