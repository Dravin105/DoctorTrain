using AutoMapper;
using DoctorTrain.Model.Dto;
using DoctorTrain.Model.Models;
using DoctorTrain.ViewModels;

namespace DoctorTrain.Profile_AutoMapper_
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDto>().ReverseMap();

            // Hospital
            CreateMap<Hospital, HospitalDto>().ReverseMap();

            // DoctorSchedule -> DoctorScheduleDto (manual doctor/hospital name)

            CreateMap<DoctorSchedule, DoctorScheduleDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.Hospital.Name));

            CreateMap<DoctorScheduleCreateDto, DoctorSchedule>();
            CreateMap<DoctorScheduleEditDto, DoctorSchedule>();
            CreateMap<DoctorSchedule, DoctorScheduleEditDto>();

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.Hospital.Name))
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patients.FirstName));
           CreateMap<Appointment, AppointmentReadDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();

        }
    }
}
