using MedService.DAL.Model;

namespace MedService.DAL.DTO
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public List<AppointmentDate> AppointmentDate { get; set; }
    }
}
