using MedService.DAL.DTO;
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
        public async Task<ActionResult<List<DoctorDTOWithoutSchedule>>> GetAllDoctors()
        {
            return await _patientService.GetAllDoctors();
        }

        [HttpGet("GetAllAppointmentTimes"), Authorize(Roles = "Admin,Doctor,Patient")]
        public async Task<ActionResult<List<TimeSpan>>> GetAllAppointmentTimes(int id, DateTime date)
        {
            return await _patientService.GetAllAppointmentTimes(id, date);
        }

        [HttpPost("MakeAnAppointment"), Authorize(Roles = "Patient")]
        public async Task<List<TimeSpan>> MakeAnAppointment([FromBody] AppointmentRequest appointmentRequest )
        {
            await _patientService.MakeAnAppointment(appointmentRequest);

            return await _patientService.GetAllAppointmentTimes(appointmentRequest.Id, appointmentRequest.Date);
        }

        [HttpDelete("DeleteAnAppointment"), Authorize(Roles = "Patient")]
        public async Task<ActionResult> DeleteAnAppointment([FromBody] AppointmentRequest appointmentRequest)
        {
            await _patientService.DeleteAnAppointment(appointmentRequest);

            return Ok();
        }
    }
}
