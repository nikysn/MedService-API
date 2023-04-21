using MedService.Contracts.Requests.Appointment;
using MedService.Contracts.Responses.Doctor;
using MedServiceAPI.Services.PatientServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedServiceAPI.Controllers
{
    [Route("Patient/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("GetAllDoctors"), Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<ActionResult<List<DoctorWithoutScheduleResponse>>> GetAllDoctors()
        {
            return await _patientService.GetAllDoctors();
        }

        [HttpGet("GetAllAppointmentTimes"), Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<ActionResult<List<TimeSpan>>> GetAllAppointmentTimes([FromRoute] int doctorId, [FromRoute] DateTime date)
        {
            return await _patientService.GetAllAppointmentTimes(doctorId, date);
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

            return Ok();
        }
    }
}
