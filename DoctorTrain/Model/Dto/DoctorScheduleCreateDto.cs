namespace DoctorTrain.Model.Dto
{
    public class DoctorScheduleCreateDto
    {
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string DayOfWeek { get; set; }
    }
}
