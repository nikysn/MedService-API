using MedService.Common.Models.Users.Doctor;

namespace MedService.DAL.Model
{
    public class ScheduleGenerator
    {
        public Dictionary<DayOfWeek, List<TimeSpan>> GenerateSchedule(Doctor doctor)
        {
            var schedule = new Dictionary<DayOfWeek, List<TimeSpan>>();
            var timeSpans = new List<TimeSpan>();

            switch (doctor.Speciality)
            {
                case DoctorSpeciality.Therapist:
                    timeSpans.AddRange(new[]
                    {
                        new TimeSpan(9,0,0),
                        new TimeSpan(10,0,0),
                        new TimeSpan(11,0,0),
                        new TimeSpan(13,0,0),
                        new TimeSpan(14,0,0),
                        new TimeSpan(15,0,0),
                    });

                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
                            continue;
                        schedule[day] = new List<TimeSpan>(timeSpans);
                    }

                    break;

                case DoctorSpeciality.Surgeon:
                    timeSpans.AddRange(new[]
                    {
                        new TimeSpan(10,0,0),
                        new TimeSpan(11,0,0),
                        new TimeSpan(12,0,0),
                        new TimeSpan(13,0,0),
                        new TimeSpan(14,0,0),
                    });

                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        if (day == DayOfWeek.Monday || day == DayOfWeek.Wednesday || day == DayOfWeek.Friday)
                            schedule[day] = new List<TimeSpan>(timeSpans);
                    }

                    break;

                default:
                    throw new ArgumentException("Unsupperted speciality");
            }
            return schedule;
        }
    }
}
