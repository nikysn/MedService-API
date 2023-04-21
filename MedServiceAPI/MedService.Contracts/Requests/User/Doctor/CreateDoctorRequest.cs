using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedService.Common.Models.Users.Doctor;

namespace MedService.Contracts.Requests.User.Doctor
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
