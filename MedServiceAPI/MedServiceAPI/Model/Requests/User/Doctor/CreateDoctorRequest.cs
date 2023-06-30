using MedService.Common.Models.Users.Doctor;

namespace MedServiceAPI.Model.Requests.User.Doctor
{
    public class CreateDoctorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpeciality Speciality { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
