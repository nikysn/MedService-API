using MedService.DAL.Model;

namespace MedService.DAL.DTO
{
    public class AppointmentDateDto
    {
        public DateTime Date { get; set; }
        public List<AppointmentTime> AppointmentTimes { get; set; }
        public int DoctorId { get; set; }
    }
}
