
using System.ComponentModel.DataAnnotations;

namespace MedService.Contracts.DTOModel.Appointment
{
    public class CreateOrDeleteAppointmentDto
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
