
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

        public AppointmentDate(DateTime dateTime,TimeSpan timeSpan, int doctorId, int patientId) : this()
        {
            Date = dateTime;
            var appointmentTime = new AppointmentTime(timeSpan, Id, patientId);
            AppointmentTimes.Add(appointmentTime);
            DoctorId = doctorId;
        }
    }
}

