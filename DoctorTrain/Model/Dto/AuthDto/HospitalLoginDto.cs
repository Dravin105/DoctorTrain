using System.ComponentModel.DataAnnotations;

namespace DoctorTrain.Model.Dto.AuthDto
{
    public class HospitalLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
