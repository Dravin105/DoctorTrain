using System.ComponentModel.DataAnnotations;

namespace DoctorTrain.Model.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Mobile { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        // Optional - agar Doctor ke patients count chahiye list me
        public int TotalAppointments { get; set; }
    }
}
