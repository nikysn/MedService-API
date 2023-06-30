using MedService.Common.Models.Users.Doctor;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedService.DAL.Model;

public enum Speciality
{
    Therapist,
    Surgeon
}

public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DoctorSpeciality Speciality { get; set; }
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public List<AppointmentDate> AppointmentDate { get; set; }

    public WorkingHours WorkingHours { get; set; }
    // [NotMapped]
    // public Dictionary<DayOfWeek, List<TimeSpan>> Schedule { get; set; }

    public Doctor(string firstName, string lastName, DoctorSpeciality speciality)
    {
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        AppointmentDate = new List<AppointmentDate>();
      //  var scheduleGenerator = new ScheduleGenerator();
       // Schedule = scheduleGenerator.GenerateSchedule(this);
      //  GenerationSchedule();

    }

 /*   private void GenerationSchedule()
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
    }*/
}
