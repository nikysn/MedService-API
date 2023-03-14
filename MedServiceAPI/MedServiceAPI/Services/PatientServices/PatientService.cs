using AutoMapper;
using MedServiceAPI.Data;
using MedServiceAPI.Dto;
using MedServiceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MedServiceAPI.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public PatientService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        private async Task<Doctor> GetDoctor(int id)
        {
            var doctor = await  _dataContext.Doctors
               .Include(d => d.AppointmentDate)
               .ThenInclude(ad => ad.AppointmentTimes)
               .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                throw new ArgumentException("Такого доктора нет");
            }

            return doctor;
        }

        public async Task<List<DoctorDTOWithoutSchedule>> GetAllDoctors()
        {
            var doctors = await _dataContext.Doctors.ToListAsync();
            var doctorsDto = _mapper.Map<List<DoctorDTOWithoutSchedule>>(doctors);
            return doctorsDto;
        }

        public async Task<List<TimeSpan>> GetAllAppointmentTimes(int id, DateTime date)
        {
            var doctor = await GetDoctor(id);

            if (date == default)
            {
                throw new ArgumentException("Дата не может быть пустой");
            }

            if (doctor.Schedule == null || !doctor.Schedule.ContainsKey(date.DayOfWeek))
            {
                throw new ArgumentException($"Расписание доктора отсутствует или У доктора нет расписания на {date.DayOfWeek}");
            }
            
              var occupiedTimes = doctor.AppointmentDate
                  .SingleOrDefault(ad => ad.Date == date)
                  ?.AppointmentTimes.Where(at => at.Time != null)
                  .ToList() ?? new List<AppointmentTime>();

              var freeTimes = doctor.Schedule[date.DayOfWeek]
                  .Where(t => !occupiedTimes.Any(ot => ot.Time == t))
                  .ToList();
            
            return freeTimes;
        }

        public async Task<List<AppointmentTime>> MakeAnAppointment(int id, DateTime date, string time)
        {
            var doctor = await GetDoctor(id);

            var appointmentDate = doctor.AppointmentDate.SingleOrDefault(ad => ad.Date == date);

            if (appointmentDate == null)
            {
                appointmentDate = new AppointmentDate(date, TimeSpan.Parse(time), doctor.Id);
                doctor.AppointmentDate.Add(appointmentDate);
            }

            else
            {
                appointmentDate.AppointmentTimes.Add(new AppointmentTime(TimeSpan.Parse(time), doctor.Id));
            }

            await _dataContext.SaveChangesAsync();

            return appointmentDate.AppointmentTimes;
        }

        
    }
}
