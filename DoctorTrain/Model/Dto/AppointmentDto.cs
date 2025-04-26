namespace DoctorTrain.Model.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string Mobile { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
    }
}
