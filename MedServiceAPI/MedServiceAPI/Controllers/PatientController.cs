using AutoMapper;
using MedService.Contracts.Abstraction.Services;
using MedService.Contracts.DTOModel.Appointment;
using MedServiceAPI.Model.Requests.Appointment;
using MedServiceAPI.Model.Responses.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedServiceAPI.Controllers
{
    [Route("Patient/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            _mapper = mapper;
        }

        [HttpGet("GetAllDoctors"), Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<ActionResult<IEnumerable<DoctorWithOutScheduleResponse>>> GetAllDoctors()
        {
            var doctors = await _patientService.GetAllDoctors();
            return Ok(_mapper.Map<IEnumerable<DoctorWithOutScheduleResponse>>(doctors));
        }

        [HttpGet("GetAllAppointmentTimes"), Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<ActionResult<IEnumerable<TimeSpan>>> GetAllAppointmentTimes([FromQuery] AppointmentRequest appointmentRequest)
        {
            var appointmentDto = _mapper.Map<AppointmentDto>(appointmentRequest);
            return await _patientService.GetAllAppointmentTimes(appointmentDto);
        }

        [HttpPost("MakeAnAppointment"), Authorize(Roles = "Patient")]
        public async Task<ActionResult<List<TimeSpan>>> MakeAnAppointment([FromBody] CreateOrDeleteAppointmentRequest createOrDeleteAppointmentRequest)
        {
            await _patientService.MakeAnAppointment(createOrDeleteAppointmentRequest);

            return await _patientService.GetAllAppointmentTimes(createOrDeleteAppointmentRequest.Id, createOrDeleteAppointmentRequest.Date);
        }

        [HttpDelete("DeleteAnAppointment"), Authorize(Roles = "Patient")]
        public async Task<ActionResult> DeleteAnAppointment([FromBody] CreateOrDeleteAppointmentRequest createOrDeleteAppointmentRequest)
        {
            await _patientService.DeleteAnAppointment(createOrDeleteAppointmentRequest);

            return NoContent();
        }
    }
}
