using DoctorTrain.Business_Layer.Interface.IAuth;
using DoctorTrain.Model.Dto.AuthDto;
using DoctorTrain.Model.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace DoctorTrain.Business_Layer.Service.AuthService
{
    public class AuthHospitalService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthHospitalService(UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager,
                                   RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> RegisterAdminAsync(AdminRegistrationDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                return "Email already registered.";

            var admin = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.MobileNo
            };

            var result = await _userManager.CreateAsync(admin, dto.Password);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(admin, "Admin");
            return "Admin registered successfully.";
        }

        public async Task<string> RegisterPatientAsync(PatientRegistrationDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                return "Email already registered.";

            var patient = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.MobileNo
            };

            var result = await _userManager.CreateAsync(patient, dto.Password);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            if (!await _roleManager.RoleExistsAsync("Patient"))
                await _roleManager.CreateAsync(new IdentityRole("Patient"));

            await _userManager.AddToRoleAsync(patient, "Patient");
            return "Patient registered successfully.";
        }

        public async Task<string> RegisterDoctorAsync(DoctorRegistrationDto dto)
        {
            var admin = await _userManager.FindByEmailAsync(dto.AdminEmail);
            if (admin == null || !await _userManager.CheckPasswordAsync(admin, dto.AdminPassword))
                return "Invalid admin credentials.";

            if (!await _userManager.IsInRoleAsync(admin, "Admin"))
                return "Unauthorized: Only Admins can register Doctors.";

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return "Doctor email already exists.";

            var doctor = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.MobileNo
            };

            var result = await _userManager.CreateAsync(doctor, dto.Password);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            if (!await _roleManager.RoleExistsAsync("Doctor"))
                await _roleManager.CreateAsync(new IdentityRole("Doctor"));

            await _userManager.AddToRoleAsync(doctor, "Doctor");
            return "Doctor registered successfully.";
        }

        public async Task<string> LoginAsync(HospitalLoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return "InvalidEmail";

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            if (!result.Succeeded)
                return "InvalidPassword";

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "No role assigned.";
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
