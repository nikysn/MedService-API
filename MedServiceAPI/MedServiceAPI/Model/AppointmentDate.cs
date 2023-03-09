using MedServiceAPI.Model;

namespace MedServiceAPI.Model
{
    public class AppointmentDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<AppointmentTime> AppointmentTimes { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        
    }
}

