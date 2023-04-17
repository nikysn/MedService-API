using FluentValidation;
using MedService.DAL.Model;

namespace MedServiceAPI.Validations
{
    public class AppointmentTimeRequestValidator : AbstractValidator<(Doctor doctor,AppointmentDate appointmentDate)>
    {
        public AppointmentTimeRequestValidator()
        {
            RuleFor(x => x.appointmentDate.AppointmentTimes[0]).NotEmpty().WithMessage("Время не может быть пустым");
            RuleFor(x => x.appointmentDate.AppointmentTimes[0]).
                 Must(time => time.Time.Minutes == 0 && time.Time.Seconds == 0)
                 .WithMessage("Неверный формат времени, нужно вводить только часы");
            RuleFor(x => x).Must(IsAppointmentTimeUnavailable).WithMessage("Время уже занято");
            RuleFor(x => x).Must(IsTimeUnavailable).WithMessage("Такого времени нет в графике");
        }

        private bool IsAppointmentTimeUnavailable((Doctor doctor, AppointmentDate appointmentDate) data)
        {
            var time = data.appointmentDate.AppointmentTimes[0].Time;
            var appointmentDate = data.appointmentDate.Date;
            var doctorAppointmentDate = data.doctor?.AppointmentDate.FirstOrDefault(ad => ad.Date.Date == appointmentDate.Date);
            if (doctorAppointmentDate == null)
            {
                return true; // доктор не работает в запрошенный день, запись на данное время свободна
            }

            return !doctorAppointmentDate.AppointmentTimes.Any(at => at.Time == time);
        }

        private bool IsTimeUnavailable((Doctor doctor,AppointmentDate appointmentDate) data)
        {
            var appointmentTime = data.appointmentDate.AppointmentTimes[0];
            var dayOfWeek = data.appointmentDate.Date.DayOfWeek;

            //получаем время работы доктора для дня недели, на который записывается пациент
            if (data.doctor?.Schedule?.TryGetValue(dayOfWeek, out var workingHours) == true)
            {
                // проверяем, есть ли время, на которое хочет записаться пациент, в графике работы доктора
                return workingHours.Any(x => x == appointmentTime.Time);
            }
            return true;
        }
    }
}
