using MapsterMapper;
using MedService.Contracts.Abstraction.Repositories;
using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;
using MedService.DAL.Data;
using MedService.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MedService.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IValidator<>
        public DoctorRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
        }

        public async Task<List<Doctor>> DeleteDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DoctorWithOutScheduleDto>> GetAllDoctors()
        {
            var doctorEntities = await _dataContext.Doctors.ToArrayAsync();
            var doctorDtos = _mapper.Map<Doctor[], DoctorWithOutScheduleDto[]>(doctorEntities);
            return doctorDtos;
        }

        public async Task<Doctor> GetDoctorByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TimeSpan>> GetAvailableTimes(AppointmentDto appointmentDto)
        {
            var availableTimes = await _dataContext.WorkingHours
                  .Where(wh => wh.DoctorId == appointmentDto.DoctorId && wh.DayOfWeek == (int)appointmentDto.Date.DayOfWeek)
                  .Select(wh => wh.AppointmentTime)
                  .Except(_dataContext.AppointmentDates
                             .Where(ad => ad.DoctorId == appointmentDto.DoctorId && ad.Date.Date == appointmentDto.Date.Date)
                             .SelectMany(ad => ad.AppointmentTimes)
                             .Select(at => at.Time))
                  .ToArrayAsync();

            return  availableTimes;
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Doctor> GetUserByLoginAsync(string userLogin)
        {
            var doctor = await _dataContext.Doctors.FirstOrDefaultAsync(u => u.Login == userLogin);

            if (doctor == null)
            {
                throw new ArgumentException("Такого доктора нет");     //  ИСПРАВИТЬ - когда добавляем первого доктора - он выдаст налл
            }
            return doctor;
        }
    }
}
