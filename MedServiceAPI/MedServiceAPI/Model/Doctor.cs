using MedServiceAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        public List<TimeSpan> GetFreeAppointmentTimesDate(DateTime date)
        {
            var occupiedTimes = AppointmentDate //AppointmentDate - это коллекция объектов AppointmentDate,
                                                //которые содержат информацию о занятых временах.
                .FirstOrDefault(ad => ad.Date == date.Date)?.AppointmentTimes //это метод Linq, который ищет первый элемент в коллекции AppointmentDate,
                                                                              //который соответствует условию, заданному в скобках.
                                                                              //В этом случае мы ищем объект AppointmentDate,
                                                                              //у которого свойство Date соответствует указанной дате date.Date.
                                                                              //
                                                                              //?.AppointmentTimes - это оператор ?. (null-условный оператор),
                                                                              //который позволяет проверить, является ли найденный объект AppointmentDate равным null,
                                                                              //прежде чем вызывать свойство AppointmentTimes.
                                                                              //Если объект AppointmentDate равен null, оператор ?. вернет null и следующие методы Linq не будут вызваны.

                .Where(at => at.Time != null) // это метод Linq, который фильтрует список времен AppointmentTimes,
                                              // оставляя только те, у которых свойство Time не равно null.
                .ToList();
            // Таким образом, в результате выполнения этого кода мы получаем список занятых времен для указанной даты date.Date,
            // который не содержит элементов со значением null в свойстве Time.

            var freeTimes = Schedule[date.DayOfWeek]  //это получение списка времен для указанного дня недели на основе расписания Schedule.
                                                      //Это означает, что мы получаем список времен для понедельника, вторника, среды и т.д.,
                                                      //в зависимости от значения свойства date.DayOfWeek.
                .Where(t => !occupiedTimes.Any(ot => ot.Time == t))
                //.Select(time => new AppointmentTime (time, 0))
                .ToList();

            return freeTimes;
        }
    }
}
