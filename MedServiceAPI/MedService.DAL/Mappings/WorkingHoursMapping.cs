using Mapster;
using MedService.Contracts.DTOModel.User.Doctor;
using MedService.DAL.Model;

namespace MedService.DAL.Mappings
{
    public class WorkingHoursMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<WorkingHours[], WorkingHours[]>();

            TypeAdapterConfig<WorkingHours[], WorkingHoursDto>
                .NewConfig()
                .Map(dest => dest.Schedule, src => src.Adapt<Dictionary<DayOfWeek, List<TimeSpan>>>());
        }
    }
}
