using AutoMapper;
using DoctorTrain.Business_Layer.Interface;
using DoctorTrain.DatabaseConection;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorTrain.Business_Layer.Service
{
    public class DoctorService: IDoctorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            try
            {
                var doctors = await _context.Doctors.ToListAsync();
                return _mapper.Map<List<DoctorDto>>(doctors);
            }
            catch (Exception ex)
            {
                // log karna chahe to kar
                throw new Exception("Error fetching doctors", ex);
            }
        }
        public async Task<DoctorDto> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            return _mapper.Map<DoctorDto>(doctor);
        }
        public async Task AddDoctorAsync(DoctorDto doctorDto)
        {
            try
            {
                var doctor = _mapper.Map<Doctor>(doctorDto);

                // Image Save Logic
                if (doctorDto.ImageFile != null && doctorDto.ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(doctorDto.ImageFile.FileName);
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "doctors");

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    var fullPath = Path.Combine(imagePath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await doctorDto.ImageFile.CopyToAsync(stream);
                    }

                    doctor.ImageUrl = "/Images/doctors/" + fileName;
                }

                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add doctor", ex);
            }
        }
        public async Task UpdateDoctorAsync(int id, DoctorDto doctorDto)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) throw new Exception("Doctor not found.");

            doctor.FullName = doctorDto.FullName;
            doctor.Specialization = doctorDto.Specialization;
            doctor.Email = doctorDto.Email;
            doctor.Mobile = doctorDto.Mobile;

            // 🖼️ Handle Image Upload
            if (doctorDto.ImageFile != null && doctorDto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "doctors");
                Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(doctorDto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await doctorDto.ImageFile.CopyToAsync(fileStream);
                }

                doctor.ImageUrl = "/Images/doctors/" + uniqueFileName;
            }

            _context.Update(doctor);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) throw new Exception("Doctor not found.");

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
