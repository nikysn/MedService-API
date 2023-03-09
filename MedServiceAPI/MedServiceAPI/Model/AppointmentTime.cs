namespace MedServiceAPI.Model
{
    public class AppointmentTime
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int AppointmentDateId { get; set; }
        public AppointmentDate appointmentDate { get; set; }
        public bool Occupied { get; set; }
    }
}
