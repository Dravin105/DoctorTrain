using DoctorTrain.Model.Dto;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<DoctorDto> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(DoctorDto doctorDto);
        Task UpdateDoctorAsync(int id, DoctorDto doctorDto);
        Task DeleteDoctorAsync(int id);
    }
}
