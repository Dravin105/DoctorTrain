using Humanizer;

namespace DoctorTrain.Model.Dto
{
    public class AppointmentReadDto
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        //public string Status { get; set; }
    }
}
