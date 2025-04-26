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
            var Hospital= await _context.Hospitals.ToListAsync();
            return _mapper.Map<List<HospitalDto>>(Hospital);
        }

        public async Task<List<DoctorDto>> GetDoctorsAsync()
        {
            var Doctor = await _context.Doctors.ToListAsync();
            return _mapper.Map<List<DoctorDto>>(Doctor);
        }
        public async Task<List<PatientDto>> GetPatientAsync()
        {
            var Doctor = await _context.Patients.ToListAsync();
            return _mapper.Map<List<PatientDto>>(Doctor);
        }
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Hospital)
                .Include(a => a.Patients)
                .ToListAsync();

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
        public async Task<AppointmentDto> GetDoctorByIdAsync(int id)
        {
            var appointment = await _context.Doctors.FindAsync(id);
            return _mapper.Map<AppointmentDto>(appointment);
        }
        public async Task CreateAsync(AppointmentDto dto)
        {
            var appointment = _mapper.Map<Appointment>(dto);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AppointmentDto appointmentDto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return;

            _mapper.Map(appointmentDto, appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Appointments.FindAsync(id);
            if (doctor == null) throw new Exception("Appointments not found.");

            _context.Appointments.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
