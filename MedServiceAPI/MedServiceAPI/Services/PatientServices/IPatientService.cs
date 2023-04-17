using MedService.DAL.DTO;
using MedService.DAL.Model;

namespace MedServiceAPI.Services.PatientServices
{
    public interface IPatientService
    {
        /// <summary>
        /// Показать всех докторов
        /// </summary>
        /// <returns></returns>
        Task<List<DoctorDTOWithoutSchedule>> GetAllDoctors();

        /// <summary>
        /// Показать свободное время для записи к врачу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<List<TimeSpan>> GetAllAppointmentTimes(int id, DateTime date);

        /// <summary>
        /// Записаться на прием
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<List<AppointmentTime>> MakeAnAppointment(AppointmentRequest appointmentRequest);

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task DeleteAnAppointment(AppointmentRequest appointmentRequest);
        
    }
}
