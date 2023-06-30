using FluentValidation;
using MapsterMapper;
using MedService.Contracts.Abstraction.Repositories;
using MedService.Contracts.Abstraction.Services;
using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;
using MedService.DAL.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using MedService.DAL.Model;

namespace MedService.BL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IValidator<AppointmentDto> _validator;

        public PatientService(IMapper mapper, IDoctorRepository doctorRepository, IAuthService authService, IValidator<AppointmentDto> validator)
        {
            _mapper = mapper;
            _doctorRepository = doctorRepository;
            _authService = authService;
            _validator = validator;
        }

        public async Task<IEnumerable<DoctorWithOutScheduleDto>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctors();
            return doctors;
        }

        public async Task<IEnumerable<TimeSpan>> GetAllAppointmentTimes(AppointmentDto appointmentDto)
        {
            _validator.ValidateAndThrow(appointmentDto);

            var availableTimes = await _doctorRepository.GetAvailableTimes(appointmentDto);

            return availableTimes;
        }

        public async Task<List<AppointmentTime>> MakeAnAppointment(CreateOrDeleteAppointmentRequest createOrDeleteAppointmentRequest)
        {
            var doctor = await _doctorRepository.GetDoctor(createOrDeleteAppointmentRequest.Id);
            var patient = await _authService.GetCurrentPatient();
            dateRequestValidator.ValidateAndThrow((doctor, createOrDeleteAppointmentRequest.Date));

            var appointmentDate = doctor.AppointmentDate.SingleOrDefault(ad => ad.Date.Date == createOrDeleteAppointmentRequest.Date.Date);
            var validatorTime = new AppointmentTimeRequestValidator();

            if (appointmentDate == null)
            {
                appointmentDate = new AppointmentDate(createOrDeleteAppointmentRequest.Date, TimeSpan.Parse(createOrDeleteAppointmentRequest.Time), doctor.Id, patient.Id);
                validatorTime.ValidateAndThrow((doctor, appointmentDate));
                doctor.AppointmentDate.Add(appointmentDate);
            }

            else
            {
                var newAppointmentDate = new AppointmentDate(createOrDeleteAppointmentRequest.Date, TimeSpan.Parse(createOrDeleteAppointmentRequest.Time), doctor.Id, patient.Id);
                validatorTime.ValidateAndThrow((doctor, newAppointmentDate));
                var appointmentTime = newAppointmentDate.AppointmentTimes[0];
                appointmentDate.AppointmentTimes.Add(appointmentTime);
            }

            await _doctorRepository.SaveChanges();

            return appointmentDate.AppointmentTimes;
        }

        public async Task DeleteAnAppointment(CreateOrDeleteAppointmentRequest createOrDeleteAppointmentRequest)
        {
            var doctor = await _doctorRepository.GetDoctor(createOrDeleteAppointmentRequest.Id);
            var patient = await _authService.GetCurrentPatient();
            var appointmentDate = doctor.AppointmentDate.SingleOrDefault(ad => ad.Date.Date == createOrDeleteAppointmentRequest.Date.Date);
            if (appointmentDate == null)
            {
                throw new ArgumentException("Запись на эту дату не найдена");
            }
            var appointmentTime = appointmentDate.AppointmentTimes.Single(at => at.Time == TimeSpan.Parse(createOrDeleteAppointmentRequest.Time));

            if (appointmentTime.PatientId != patient.Id)
            {
                throw new ArgumentException("Вы не можете удалить чужую запись");
            }
            appointmentDate.AppointmentTimes.Remove(appointmentTime);

            await _doctorRepository.SaveChanges();
        }
    }
}
