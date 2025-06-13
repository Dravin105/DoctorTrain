using DoctorTrain.Business_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTrain.Controllers.Doctor
{
    [Authorize(Roles = "Doctor")]
    public class DoctorsController : Controller
    {
        private readonly IDoctorScheduleService _service;
        private readonly IAppointmentService _iAppointmentService;
        private readonly IPatientService _patientService;
        private readonly IHospitalService _hospitalservice;
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorScheduleService service,
            IAppointmentService iAppointmentService, IPatientService patientService,
            IHospitalService hospitalservice, IDoctorService doctorService)
        {
            _service = service;
            _iAppointmentService= iAppointmentService;
            _patientService= patientService;
            _hospitalservice = hospitalservice;
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult>   SchedulList()
        {
            var schedules = await _service.GetAllAsync();
            return View(schedules);
        
        }
        public async Task<IActionResult> AppointmentList()
        {
            var Appointment = await _iAppointmentService.GetAllAppointmentsAsync();
            return View(Appointment);
        }
        public async Task<IActionResult> HospitalsList()
        {
            var hospital = await _hospitalservice.GetAllAsync();
            return View(hospital);

        }
        public async Task<IActionResult> DoctorsList()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            foreach (var doc in doctors)
            {
                doc.TotalAppointments = await _iAppointmentService.GetAppointmentCountByDoctorIdAsync(doc.Id);
            }

            return View(doctors);

        }
        public async Task<IActionResult> PatientsList()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients);

        }
        public async Task<IActionResult> PatientsByDoctor(int doctorId)
        {
            var patients = await _iAppointmentService.GetPatientsByDoctorIdAsync(doctorId);
            return View(patients);
        }
    }
}

