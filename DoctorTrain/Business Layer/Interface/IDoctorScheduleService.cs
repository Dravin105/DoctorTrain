using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IDoctorScheduleService
    {
        Task<List<DoctorScheduleDto>> GetAllAsync();
        Task<DoctorScheduleDto> GetByIdAsync(int id);
        Task CreateAsync(DoctorScheduleCreateDto dto);
        Task UpdateAsync(int id, DoctorScheduleEditDto dto);
        Task DeleteAsync(int id);
        Task<List<DoctorDto>> GetDoctorsAsync();
        Task<List<HospitalDto>> GetHospitalAsync();
        Task<List<DoctorScheduleDto>> GetSchedulesByDoctorIdAsync(int doctorId);
    }
}
