namespace DoctorTrain.Model.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }

        public TimeSpan ArrivalTime { get; set; }      // Kab hospital me aayega
        public TimeSpan DepartureTime { get; set; }    // Kab hospital se jayega
        public string DayOfWeek { get; set; }          // Kis din (Monday, Tuesday...)

        public Doctor Doctor { get; set; }             // Navigation for include()
        public Hospital Hospital { get; set; }
    }
}
