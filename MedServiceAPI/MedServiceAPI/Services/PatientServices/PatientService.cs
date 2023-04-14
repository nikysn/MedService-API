using AutoMapper;
using Azure.Core;
using FluentValidation;
using MedService.DAL.Model;
using MedService.DAL.Interfaces;
using MedService.DAL.Data;
using MedServiceAPI.Validations;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MedService.DAL.DTO;
using MedServiceAPI.Services.AdminService;

namespace MedServiceAPI.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private AppointmentDateRequestValidator dateRequestValidator = new AppointmentDateRequestValidator();
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public PatientService(IMapper mapper, IDoctorRepository doctorRepository, IAuthService authService)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _authService = authService;
        }

        public async Task<List<DoctorDTOWithoutSchedule>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctors();
            var doctorsDto = _mapper.Map<List<DoctorDTOWithoutSchedule>>(doctors);
            return doctorsDto;
        }

        public async Task<List<TimeSpan>> GetAllAppointmentTimes(int id, DateTime date)
        {
            var doctor = await _doctorRepository.GetDoctor(id);

            dateRequestValidator.ValidateAndThrow((doctor, date));

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
            var doctor = await _doctorRepository.GetDoctor(id);
            var patient = await _authService.GetCurrentPatient();
            dateRequestValidator.ValidateAndThrow((doctor, date));

            var appointmentDate = doctor.AppointmentDate.SingleOrDefault(ad => ad.Date == date);
            var validatorTime = new AppointmentTimeRequestValidator();

            if (appointmentDate == null)
            {
                appointmentDate = new AppointmentDate(date, TimeSpan.Parse(time), doctor.Id, patient.Id);
                validatorTime.ValidateAndThrow((doctor, appointmentDate));
                doctor.AppointmentDate.Add(appointmentDate);
            }

            else
            {
                var newAppointmentDate = new AppointmentDate(date, TimeSpan.Parse(time), doctor.Id, patient.Id);
                validatorTime.ValidateAndThrow((doctor, newAppointmentDate));
                var appointmentTime = newAppointmentDate.AppointmentTimes[0];
                appointmentDate.AppointmentTimes.Add(appointmentTime);
            }

            await _doctorRepository.SaveChanges();

            return appointmentDate.AppointmentTimes;
        }

        public async Task DeleteAnAppointment(int id, DateTime date, string time)
        {
            var doctor = await _doctorRepository.GetDoctor(id);
            var appointmentDate = doctor.AppointmentDate.Single(ad => ad.Date == date);
            var appointmentTime = appointmentDate.AppointmentTimes.Single(at => at.Time == TimeSpan.Parse(time));



            appointmentDate.AppointmentTimes.Remove(appointmentTime);

            await _doctorRepository.SaveChanges();
        }

    }
}
