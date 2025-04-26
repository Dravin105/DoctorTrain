using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Business_Layer.Service;
using DoctorTrain.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var patient = await _patientService.GetAllPatientsAsync();
            return View(patient);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PatientDto dto)
        {
            if (ModelState.IsValid)
            {
                await _patientService.CreatePatientAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var patient= await _patientService.GetPatientByIdAsync(id);
            return View(patient);
        }

        // POST: /Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientDto patientDto)
        {
            if (ModelState.IsValid)
            {
                await _patientService.UpdatePatientAsync(id, patientDto);
                return RedirectToAction(nameof(Index));
            }
            return View(patientDto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _patientService.GetPatientByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _patientService.GetPatientByIdAsync(id);
            return View(hospital);
        }
    }
}
