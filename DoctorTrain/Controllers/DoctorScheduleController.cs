using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers
{
    public class DoctorScheduleController : Controller
    {
        private readonly IDoctorScheduleService _service;

        public DoctorScheduleController(IDoctorScheduleService service)
        {
            _service = service;
        }

        // Get all doctor schedules
        public async Task<IActionResult> Index()
        {
            var schedules = await _service.GetAllAsync();
            return View(schedules);
        }

        // Get doctor schedule by Id
        public async Task<IActionResult> Details(int id)
        {
            var schedule = await _service.GetByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // Create new doctor schedule
        public async Task<IActionResult> Create()
        {
            ViewBag.Doctors = await _service.GetDoctorsAsync();
            ViewBag.Hospitals = await _service.GetHospitalAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorScheduleCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // Edit doctor schedule
        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _service.GetByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var dto = new DoctorScheduleEditDto
            {
                DoctorId = schedule.DoctorId,
                HospitalId = schedule.HospitalId,
                ArrivalTime = schedule.ArrivalTime,
                DepartureTime = schedule.DepartureTime,
                DayOfWeek = schedule.DayOfWeek
            };
            ViewBag.Doctors = await _service.GetDoctorsAsync();
            ViewBag.Hospitals = await _service.GetHospitalAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorScheduleEditDto dto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // Delete doctor schedule
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _service.GetByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> StatesView(int id)
        {
            var doctors = await _service.GetSchedulesByDoctorIdAsync(id);
            return View(doctors);
        }
    }
}