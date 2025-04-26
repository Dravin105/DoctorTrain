using System.ComponentModel.DataAnnotations.Schema;

namespace DoctorTrain.Model.Models
{
    public class Appointment
    {
        public int Id { get; set; }                // Unique ID
        public string Mobile { get; set; }    // Patient ka Mobile
        public int DoctorId { get; set; }          // Doctor jiske paas jaa raha hai
        public Doctor Doctor { get; set; }

        public int HospitalId { get; set; }        // Hospital ka ID
        public Hospital Hospital { get; set; }

        public int PatientId { get; set; }
        public Patient Patients { get; set; }

        public DateTime Date { get; set; }         // Appointment ki date
        public TimeSpan Time { get; set; }         // Appointment ka time
        public string Status { get; set; } = "Pending";
    }
}
