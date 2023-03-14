using MedServiceAPI.Dto;
using MedServiceAPI.Model;
using MedServiceAPI.Services.PatientServices;
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

        [HttpGet]
        public async Task<ActionResult<List<DoctorDTOWithoutSchedule>>> GetAllDoctors()
        {
            return await _patientService.GetAllDoctors();
        }

        [HttpGet("GetAllAppointmentTimes")]
        public async Task<ActionResult<List<TimeSpan>>> GetAllAppointmentTimes(int id, DateTime date)
        {
            return await _patientService.GetAllAppointmentTimes(id, date);
        }

        [HttpPost]
        public async Task<List<TimeSpan>> MakeAnAppointment(int id,DateTime date,string time)
        {
            await _patientService.MakeAnAppointment(id, date, time);

            return await _patientService.GetAllAppointmentTimes(id, date);
        }
    }
}
