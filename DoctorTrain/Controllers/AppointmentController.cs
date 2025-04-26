using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Business_Layer.Service;
using DoctorTrain.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrain.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _iAppointmentService;

        public AppointmentController(IAppointmentService iAppointmentService)
        {
            _iAppointmentService = iAppointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var Appointment= await _iAppointmentService.GetAllAsync();
            return View(Appointment);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Doctors = await _iAppointmentService.GetDoctorsAsync();
            ViewBag.Hospitals = await _iAppointmentService.GetHospitalsAsync();
            ViewBag.Patients = await _iAppointmentService.GetPatientAsync();

            return View();
        }

        // POST: /Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentDto dto)
        {
            if (ModelState.IsValid)
            {
                await _iAppointmentService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
        public async Task<IActionResult> Edit()
        {
            ViewBag.Doctors = await _iAppointmentService.GetDoctorsAsync();
            ViewBag.Hospitals = await _iAppointmentService.GetHospitalsAsync();
            ViewBag.Patients = await _iAppointmentService.GetPatientAsync();

            return View();
        }

        // POST: /Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id,AppointmentDto appointmentDto)
        {
            if (id != appointmentDto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _iAppointmentService.UpdateAsync(id,appointmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentDto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _iAppointmentService.GetDoctorByIdAsync(id);
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
            await _iAppointmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _iAppointmentService.GetDoctorByIdAsync(id);
            return View(hospital);
        }
    }
}
