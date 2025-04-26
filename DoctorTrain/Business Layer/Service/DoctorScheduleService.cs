using AutoMapper;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.DatabaseConection;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DoctorTrain.Business_Layer.Service
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorScheduleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DoctorDto>> GetDoctorsAsync()
        {
            var Doct = await _context.Doctors.ToListAsync();
            var DoctorDto = _mapper.Map<List<DoctorDto>>(Doct);
            return DoctorDto;
        }
        public async Task<List<HospitalDto>> GetHospitalAsync()
        {
            var host= await _context.Hospitals.ToListAsync();
            var HospitalsDto= _mapper.Map <List<HospitalDto>>(host);
            return HospitalsDto;
        }
        // Get all doctor schedules
        public async Task<List<DoctorScheduleDto>> GetAllAsync()
        {
            var doctorSchedules = await _context.DoctorSchedules
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Hospital)
                .ToListAsync();

            return _mapper.Map<List<DoctorScheduleDto>>(doctorSchedules);
        }

        // Get doctor schedule by Id
        public async Task<DoctorScheduleDto> GetByIdAsync(int id)
        {
            var schedule = await _context.DoctorSchedules
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Hospital)
                .FirstOrDefaultAsync(ds => ds.Id == id);

            if (schedule == null)
                return null;

            return _mapper.Map<DoctorScheduleDto>(schedule);
        }

        // Create a new doctor schedule
        public async Task CreateAsync(DoctorScheduleCreateDto dto)
        {
            var schedule = _mapper.Map<DoctorSchedule>(dto);
            _context.DoctorSchedules.Add(schedule);
            await _context.SaveChangesAsync();
        }

        // Update an existing doctor schedule
        public async Task UpdateAsync(int id, DoctorScheduleEditDto dto)
        {
            var schedule = await _context.DoctorSchedules.FindAsync(id);
            if (schedule == null)
                return;

            _mapper.Map(dto, schedule);
            await _context.SaveChangesAsync();
        }

        // Delete doctor schedule by Id
        public async Task DeleteAsync(int id)
        {
            var schedule = await _context.DoctorSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.DoctorSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
