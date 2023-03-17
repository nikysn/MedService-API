using AutoMapper;
using MedServiceAPI.Dto;
using MedServiceAPI.Model;

namespace MedServiceAPI.Maps
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Doctor, DoctorDTOWithoutSchedule>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Speciality));

            CreateMap<Speciality, string>().ConvertUsing(src => src.ToString());
        }
    }
}
