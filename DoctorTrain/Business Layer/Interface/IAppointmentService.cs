using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto> GetDoctorByIdAsync(int id);
        Task CreateAsync(AppointmentDto dto);
        Task UpdateAsync(int id, AppointmentDto dto);
        Task DeleteAsync(int id);
        Task<List<HospitalDto>> GetHospitalsAsync();
        Task<List<DoctorDto>> GetDoctorsAsync();
        Task<List<PatientDto>> GetPatientAsync();
    }
}
