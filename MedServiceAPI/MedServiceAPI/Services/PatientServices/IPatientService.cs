using MedServiceAPI.Dto;
using MedServiceAPI.Model;

namespace MedServiceAPI.Services.PatientServices
{
    public interface IPatientService
    {
        Task<List<DoctorDTOWithoutSchedule>> GetAllDoctors();
        Task<List<TimeSpan>> GetAllAppointmentTimes(int id, DateTime date);
        Task<List<AppointmentTime>> MakeAnAppointment(int id, DateTime date, string time);
        
    }
}
