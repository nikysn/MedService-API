using System.ComponentModel.DataAnnotations.Schema;

namespace MedServiceAPI.Model
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public List<AppointmentDate> AppointmentDate { get; set; }

        [NotMapped]
        public Dictionary<DayOfWeek, List<TimeSpan>> Schedule { get; set; }

        public Doctor(string firstName, string lastName, string speciality)
        {
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
            AppointmentDate = new List<AppointmentDate>();

            GenerationSchedule();
            
        }

        private void GenerationSchedule()
        {
            Schedule = new Dictionary<DayOfWeek, List<TimeSpan>>();

            List<TimeSpan> timeSpans = new List<TimeSpan>
            {
                new TimeSpan(9, 0, 0),
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(13, 0, 0),
                new TimeSpan(14, 0, 0),
                new TimeSpan(15, 0, 0),

            };

            for (DayOfWeek day = DayOfWeek.Monday; day <= DayOfWeek.Friday; day++)
            {
                Schedule[day] = new List<TimeSpan>();
                Schedule[day].AddRange(timeSpans);
            }
        }
    }
}
