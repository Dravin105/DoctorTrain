using DoctorTrain.Model.Dto;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IHospitalService
    {
        Task<List<HospitalDto>> GetAllAsync();
        Task<HospitalDto> GetByIdAsync(int id);
        Task CreateAsync(HospitalDto hospitalDto);
        Task UpdateAsync(int id, HospitalDto hospitalDto);
        Task DeleteAsync(int id);
    }
}
