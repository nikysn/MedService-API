using System.ComponentModel.DataAnnotations;

namespace MedServiceAPI.Model.Requests.Appointment
{
    public class CreateOrDeleteAppointmentRequest
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
