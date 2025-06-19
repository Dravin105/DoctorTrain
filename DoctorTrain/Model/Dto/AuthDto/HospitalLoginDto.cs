using System.ComponentModel.DataAnnotations;

namespace DoctorTrain.Model.Dto.AuthDto
{
    public class HospitalLoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Only Gmail addresses are allowed.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must be 6+ chars with uppercase, lowercase, number & special char.")]
        public string Password { get; set; }
    }
}
