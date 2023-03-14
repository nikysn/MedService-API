using MedServiceAPI.Model;

namespace MedServiceAPI.Dto
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public List<AppointmentDate> AppointmentDate { get; set; }
    }
}
