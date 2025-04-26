using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IHospitalService _service;

        public HospitalController(IHospitalService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var hospitals = await _service.GetAllAsync();
            return View(hospitals);
        }

        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _service.GetByIdAsync(id);
            return View(hospital);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(HospitalDto hospitalDto)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(hospitalDto);
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _service.GetByIdAsync(id);
            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HospitalDto hospitalDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, hospitalDto);
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _service.GetByIdAsync(id);
            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
