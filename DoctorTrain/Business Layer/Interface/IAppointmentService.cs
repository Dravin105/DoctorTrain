using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;

namespace DoctorTrain.Business_Layer.Interface
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<Appointment> CreateAsync(AppointmentReadDto dto);
        Task UpdateAsync(int id, AppointmentReadDto dto);
        Task<List<HospitalDto>> GetHospitalsAsync();
        Task<List<DoctorDto>> GetDoctorsAsync();
        Task<List<PatientDto>> GetPatientAsync();
        Task<int> GetAppointmentCountByDoctorIdAsync(int doctorId);
        Task<List<PatientDto>> GetPatientsByDoctorIdAsync(int doctorId);
    }
}
