using DoctorTrain.Model.Dto;

namespace DoctorTrain.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<DoctorDto> Doctors { get; set; }
        public ContactFormDto ContactForm { get; set; }
    }
}
