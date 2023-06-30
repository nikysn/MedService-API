using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;

namespace MedService.Contracts.Abstraction.Repositories
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(DoctorWithOutScheduleDto doctor);
        Task<IEnumerable<TimeSpan>> GetAvailableTimes(AppointmentDto appointmentDto);
        Task<IEnumerable<DoctorWithOutScheduleDto>> GetAllDoctors();
        Task<List<DoctorWithOutScheduleDto>> DeleteDoctor(DoctorWithOutScheduleDto doctor);
        Task<DoctorWithOutScheduleDto> GetDoctorByLastName(string lastName);

    }
}
