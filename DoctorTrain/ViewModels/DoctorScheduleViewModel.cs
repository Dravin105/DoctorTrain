using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DoctorTrain.ViewModels
{
    public class DoctorScheduleViewModel
    {

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int HospitalId { get; set; }

        [Required]
        public TimeSpan ArrivalTime { get; set; }

        [Required]
        public TimeSpan DepartureTime { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        // Dropdowns
        public List<SelectListItem> Doctors { get; set; }
        public List<SelectListItem> Hospitals { get; set; }
    }
}
