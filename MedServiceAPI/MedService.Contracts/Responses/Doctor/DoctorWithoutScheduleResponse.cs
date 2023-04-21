using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.Responses.Doctor
{
    public class DoctorWithoutScheduleResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
    }
}
