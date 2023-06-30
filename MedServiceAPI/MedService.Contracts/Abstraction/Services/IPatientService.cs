using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;

namespace MedService.Contracts.Abstraction.Services
{
    public interface IPatientService
    {
        /// <summary>
        /// Показать всех докторов
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DoctorWithOutScheduleDto>> GetAllDoctors();

        /// <summary>
        /// Показать свободное время для записи к врачу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<List<TimeSpan>> GetAllAppointmentTimes(AppointmentDto appointmentDto);

        /// <summary>
        /// Записаться на прием
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<List<AppointmentTime>> MakeAnAppointment(CreateOrDeleteAppointmentRequest createOrDeleteAppointmentRequest);

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        Task DeleteAnAppointment(CreateOrDeleteAppointmentRequest createOrDeleteAppointmentRequest);
    }
}
