using AutoMapper;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.DatabaseConection;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrain.Business_Layer.Service
{
    public class HospitalService: IHospitalService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HospitalService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HospitalDto>> GetAllAsync()
        {
            var hospitals = await _context.Hospitals.ToListAsync();
            return _mapper.Map<List<HospitalDto>>(hospitals);
        }

        public async Task<HospitalDto> GetByIdAsync(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            return _mapper.Map<HospitalDto>(hospital);
        }

        public async Task CreateAsync(HospitalDto hospitalDto)
        {
            var hospital = _mapper.Map<Hospital>(hospitalDto);
            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, HospitalDto hospitalDto)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null) return;

            _mapper.Map(hospitalDto, hospital);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null) return;

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
        }
    }
}
