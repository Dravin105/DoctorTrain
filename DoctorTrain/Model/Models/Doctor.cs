namespace DoctorTrain.Model.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }
        public string AboutDescription { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
