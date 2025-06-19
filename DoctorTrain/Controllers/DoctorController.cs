using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoctorDto doctorDto)
        {
            if (!ModelState.IsValid)
                return View(doctorDto);

            await _doctorService.AddDoctorAsync(doctorDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorDto doctorDto)
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


        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
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
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _doctorService.GetDoctorByIdAsync(id);
            return View(hospital);
        }
    }
}

