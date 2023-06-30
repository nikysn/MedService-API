using Mapster;
using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;
using MedServiceAPI.Model.Requests.Appointment;
using MedServiceAPI.Model.Responses.Doctor;

namespace MedServiceAPI.Mapping
{
    public class DoctorMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<DoctorWithOutScheduleDto, DoctorWithOutScheduleResponse>();
            config.ForType<AppointmentDto, AppointmentRequest>();
        }
    }
}
