namespace MedServiceAPI.Model
{
    public class AppointmentTime
    {
        public int Id { get; set; }

        public TimeSpan Time { get; set; }
        public int AppointmentDateId { get; set; }
        public AppointmentDate AppointmentDate { get; set; }

        public AppointmentTime()
        {

        }

        public AppointmentTime(TimeSpan timeSpan, int appointmentDateId)
        {
            Time= timeSpan;
            AppointmentDateId = appointmentDateId;
        }
    }
}
