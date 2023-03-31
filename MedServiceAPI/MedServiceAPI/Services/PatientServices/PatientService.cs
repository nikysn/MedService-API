using AutoMapper;
using FluentValidation;
using MedServiceAPI.Data;
using MedServiceAPI.Dto;
using MedServiceAPI.Model;
using MedServiceAPI.Validations;
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

        public async Task<List<DoctorDTOWithoutSchedule>> GetAllDoctors()
        {
            var doctors = await _dataContext.Doctors.ToListAsync();
            var doctorsDto = _mapper.Map<List<DoctorDTOWithoutSchedule>>(doctors);
            return doctorsDto;
        }
        
        public async Task<List<TimeSpan>> GetAllAppointmentTimes(int id, DateTime date)
        {
            var doctor = await GetDoctor(id);

            var validator = new AppointmentDateRequestValidator();
            validator.ValidateAndThrow((doctor, date));
           /* if (date == default)
            {
                throw new ArgumentException("Дата не может быть пустой");
            }
            
            if (doctor.Schedule == null || !doctor.Schedule.ContainsKey(date.DayOfWeek))
            {
                throw new ArgumentException($"Расписание доктора отсутствует или У доктора нет расписания на {date.DayOfWeek}");
            }

            if(date.AddDays(7) < DateTime.Now)
            {
                throw new ArgumentException("Запись к врачу только на следующие 7 дней");
            }*/

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

        public async Task DeleteAnAppointment(int id, DateTime date, string time)
        {
            var doctor = await GetDoctor(id);
            var appointmentDate = doctor.AppointmentDate.Single(ad => ad.Date == date);
            var appointmentTime = appointmentDate.AppointmentTimes.Single(at => at.Time == TimeSpan.Parse(time));
          
            appointmentDate.AppointmentTimes.Remove(appointmentTime);

            await _dataContext.SaveChangesAsync();
        }

        private async Task<Doctor> GetDoctor(int id)
        {
            var doctor = await _dataContext.Doctors
               .Include(d => d.AppointmentDate)
               .ThenInclude(ad => ad.AppointmentTimes)
               .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
            {
                throw new ArgumentException("Такого доктора нет");
            }

            return doctor;
        }
    }
}
