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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _iAppointmentService.GetAllAppointmentsAsync();
            return View(result);
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
        public async Task<IActionResult> Create(AppointmentReadDto dto)
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

        // POST: /Appointment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentReadDto appointmentDto) // Use AppointmentDto here
        {
            if (id != appointmentDto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _iAppointmentService.UpdateAsync(id, appointmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentDto); // Return same model type to the view
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _iAppointmentService.GetAppointmentByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _iAppointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View (appointment);


        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _iAppointmentService.DeleteAppointmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
        //}

        //// POST: Doctor/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _iAppointmentService.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}
        //public async Task<IActionResult> Details(int id)
        //{
        //    var hospital = await _iAppointmentService.GetDoctorByIdAsync(id);
        //    return View(hospital);
        //}
    }
}
