using MedServiceAPI.Model;

namespace MedServiceAPI.Dto
{
    public class NewDoctor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Speciality Speciality { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
