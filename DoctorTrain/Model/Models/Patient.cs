using System.ComponentModel.DataAnnotations;

namespace DoctorTrain.Model.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }              // Unique Patient ID
        public string FirstName { get; set; }     // Patient ka pehla naam
        public string Address { get; set; }      // Patient ka Address
        public string Gender { get; set; }      // Patient ka Gender
        public DateOnly DOB { get; set; }      // Patient ka DOB
        public string BloodGruop { get; set; }      // Patient ka BloodGruop
        public string Mobile { get; set; }        // Patient ka mobile number
        public string Email { get; set; }         // Patient ka email

        public ICollection<Appointment> Appointments { get; set; } // Appointments booked by patient
    }
}
