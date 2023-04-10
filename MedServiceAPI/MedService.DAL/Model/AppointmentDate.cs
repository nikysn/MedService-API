
namespace MedService.DAL.Model
{
    public class AppointmentDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
       // [NotMapped]
        public List<AppointmentTime> AppointmentTimes { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public AppointmentDate()
        {
            AppointmentTimes = new List<AppointmentTime>();
        }

        public AppointmentDate(DateTime dateTime,TimeSpan timeSpan, int doctorId) : this()
        {
            Date = dateTime;
            var appointmentTime = new AppointmentTime(timeSpan, Id);
            AppointmentTimes.Add(appointmentTime);
            DoctorId = doctorId;
        }
    }
}

