using AutoMapper;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.DatabaseConection;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrain.Business_Layer.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<HospitalDto>> GetHospitalsAsync()
        {
            var Hospital = await _context.Hospitals.ToListAsync();
            return _mapper.Map<List<HospitalDto>>(Hospital);
        }

        public async Task<List<DoctorDto>> GetDoctorsAsync()
        {
            var Doctor = await _context.Doctors.ToListAsync();
            return _mapper.Map<List<DoctorDto>>(Doctor);
        }
        public async Task<List<PatientDto>> GetPatientAsync()
        {
            var Patient = await _context.Patients.ToListAsync();
            return _mapper.Map<List<PatientDto>>(Patient);
        }
        public async Task<List<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Hospital)
                .Include(a => a.Patients)
                .ToListAsync();

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Hospital)
                .Include(a => a.Patients)
                .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AppointmentDto>(appointment);
        }
        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateAsync(AppointmentReadDto dto)
        {
            var appointment = _mapper.Map<Appointment>(dto);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AppointmentReadDto appointmentDto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return;

            _mapper.Map(appointmentDto, appointment);
            await _context.SaveChangesAsync();
        }
    }
}
//        public async Task<AppointmentDto> GetDoctorByIdAsync(int id)
//        {
//            var appointment = await _context.Appointments.FindAsync(id);
//            return _mapper.Map<AppointmentDto>(appointment);
//        }

//        

//        public async Task DeleteAsync(int id)
//        {
//            var doctor = await _context.Appointments.FindAsync(id);
//            if (doctor == null) throw new Exception("Appointments not found.");

//            _context.Appointments.Remove(doctor);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
