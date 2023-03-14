using AutoMapper;
using MedServiceAPI.Dto;
using MedServiceAPI.Model;

namespace MedServiceAPI.Maps
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Doctor, DoctorDTOWithoutSchedule>();
        }
    }
}
