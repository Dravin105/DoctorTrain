using DoctorTrain.Model.Dto;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task CreatePatientAsync(PatientDto dto);
        Task UpdatePatientAsync(int id, PatientDto dto);
        Task DeletePatientAsync(int id);
    }
}
