namespace MedServiceAPI.Model.Requests.Appointment
{
    public class AppointmentRequest
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}
