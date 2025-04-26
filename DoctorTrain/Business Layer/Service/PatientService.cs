using AutoMapper;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.DatabaseConection;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrain.Business_Layer.Service
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return _mapper.Map<List<PatientDto>>(patients);
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return patient == null ? null : _mapper.Map<PatientDto>(patient);
        }

        public async Task CreatePatientAsync(PatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(int id, PatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return;
            _mapper.Map(dto, patient);
            await _context.SaveChangesAsync();
            

        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return;
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            
        }
    }
}

