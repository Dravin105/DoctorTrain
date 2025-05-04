using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<bool> DeleteAppointmentAsync(int id);
        Task CreateAsync(AppointmentReadDto dto);
        Task UpdateAsync(int id, AppointmentReadDto dto);
        Task<List<HospitalDto>> GetHospitalsAsync();
        Task<List<DoctorDto>> GetDoctorsAsync();
        Task<List<PatientDto>> GetPatientAsync();
    }
}
