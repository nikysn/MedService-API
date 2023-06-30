
namespace MedService.DAL.Model
{
    public class WorkingHours
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan AppointmentTime { get; set; }
    }
}
