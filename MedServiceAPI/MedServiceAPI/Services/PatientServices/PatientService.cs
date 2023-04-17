using AutoMapper;
using FluentValidation;
using MedService.DAL.Model;
using MedService.DAL.Interfaces;
using MedServiceAPI.Validations;
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

        public async Task<List<AppointmentTime>> MakeAnAppointment(AppointmentRequest appointmentRequest)
        {
            var doctor = await _doctorRepository.GetDoctor(appointmentRequest.Id);
            var patient = await _authService.GetCurrentPatient();
            dateRequestValidator.ValidateAndThrow((doctor, appointmentRequest.Date));

            var appointmentDate = doctor.AppointmentDate.SingleOrDefault(ad => ad.Date.Date == appointmentRequest.Date.Date);
            var validatorTime = new AppointmentTimeRequestValidator();

            if (appointmentDate == null)
            {
                appointmentDate = new AppointmentDate(appointmentRequest.Date, TimeSpan.Parse(appointmentRequest.Time), doctor.Id, patient.Id);
                validatorTime.ValidateAndThrow((doctor, appointmentDate));
                doctor.AppointmentDate.Add(appointmentDate);
            }

            else
            {
                var newAppointmentDate = new AppointmentDate(appointmentRequest.Date, TimeSpan.Parse(appointmentRequest.Time), doctor.Id, patient.Id);
                validatorTime.ValidateAndThrow((doctor, newAppointmentDate));
                var appointmentTime = newAppointmentDate.AppointmentTimes[0];
                appointmentDate.AppointmentTimes.Add(appointmentTime);
            }

            await _doctorRepository.SaveChanges();

            return appointmentDate.AppointmentTimes;
        }

        public async Task DeleteAnAppointment(AppointmentRequest appointmentRequest)
        {
            var doctor = await _doctorRepository.GetDoctor(appointmentRequest.Id);
            var patient = await _authService.GetCurrentPatient();
            var appointmentDate = doctor.AppointmentDate.SingleOrDefault(ad => ad.Date.Date == appointmentRequest.Date.Date);
            if(appointmentDate == null)
            {
                throw new ArgumentException("Запись на эту дату не найдена");
            }
            var appointmentTime = appointmentDate.AppointmentTimes.Single(at => at.Time == TimeSpan.Parse(appointmentRequest.Time));

            if(appointmentTime.PatientId != patient.Id)
            {
                throw new ArgumentException("Вы не можете удалить чужую запись");
            }
            appointmentDate.AppointmentTimes.Remove(appointmentTime);

            await _doctorRepository.SaveChanges();
        }
    }
}
