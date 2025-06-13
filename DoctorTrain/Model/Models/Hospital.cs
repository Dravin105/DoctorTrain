namespace DoctorTrain.Model.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string ContactLocation { get; set; }

        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
