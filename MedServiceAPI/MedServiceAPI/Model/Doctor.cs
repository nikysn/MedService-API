using MedServiceAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedServiceAPI.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public List<AppointmentDate> AppointmentDate { get; set; }

        public Doctor(string firstName, string lastName, string speciality)
        {
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
        }
    }
}
