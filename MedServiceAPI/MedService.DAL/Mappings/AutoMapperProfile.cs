using AutoMapper;
using MedService.Common.Models.Users.Doctor;
using MedService.Contracts.Responses.Doctor;
using MedService.DAL.DTO;
using MedService.DAL.Model;

namespace MedService.DAL.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Doctor, DoctorWithoutScheduleResponse>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Speciality));

            CreateMap<DoctorSpeciality, string>().ConvertUsing(src => src.ToString());
        }
    }
}
