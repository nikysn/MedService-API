
using MedService.Common.Models.Users.Doctor;

namespace MedService.Contracts.DTOModel.User.Doctor
{
    public class CreateDoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorSpeciality Speciality { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
