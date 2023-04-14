namespace MedService.DAL.Model
{
    public class AppointmentTime
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }
        public int AppointmentDateId { get; set; }
        public AppointmentDate AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public AppointmentTime()
        {

        }

        public AppointmentTime(TimeSpan timeSpan, int appointmentDateId, int patientId)
        {
            Time= timeSpan;
            AppointmentDateId = appointmentDateId;
            PatientId = patientId;
        }
    }
}
