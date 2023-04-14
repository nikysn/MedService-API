using AutoMapper;
using MedService.DAL.DTO;
using MedService.DAL.Model;

namespace MedService.DAL.Mappings
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
