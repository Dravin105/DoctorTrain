using DoctorTrain.Model.Dto.AuthDto;

namespace DoctorTrain.Business_Layer.Interface.IAuth
{
    public interface IAuthService
    {
        Task<string> RegisterAdminAsync(AdminRegistrationDto dto);
        Task<string> RegisterDoctorAsync(DoctorRegistrationDto dto);
        Task<string> RegisterPatientAsync(PatientRegistrationDto dto);
        Task<string> LoginAsync(HospitalLoginDto dto);
        Task LogoutAsync();
    }
}
