using MedServiceAPI.Model;

namespace MedServiceAPI.Dto
{
    public class AppointmentDateDto
    {
        public DateTime Date { get; set; }
        public List<AppointmentTime> AppointmentTimes { get; set; }
        public int DoctorId { get; set; }
    }
}
