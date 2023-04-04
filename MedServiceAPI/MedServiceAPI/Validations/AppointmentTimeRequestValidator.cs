using FluentValidation;
using MedServiceAPI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System;

namespace MedServiceAPI.Validations
{
    public class AppointmentTimeRequestValidator : AbstractValidator<(AppointmentDate appointmentDate, string time)>
    {
        public AppointmentTimeRequestValidator()
        {
            RuleFor(x => x.time).NotEmpty().WithMessage("Дата не может быть пустой");
            RuleFor(x => x.time).
                 Must(time => TimeSpan.TryParse(time, out var appointmentTime)
                 && appointmentTime.Minutes == 0
                 && appointmentTime.Seconds == 0).WithMessage("Неверный формат времени, нужно вводить только часы");
            RuleFor(x => x).Must(Time).WithMessage("Время уже занято");
            RuleFor(x => x).Must(IsTimeAvailable).WithMessage("Такого времени нет в графике");
        }

        private bool Time((AppointmentDate appointmentDate, string time) data)
        {
            var appointmentTime = TimeSpan.Parse(data.time);
            if (data.appointmentDate.AppointmentTimes.Any(x => x.Time== appointmentTime))
            {
                return false;
            }

            return true;
        }

        private bool IsTimeAvailable((AppointmentDate appointmentDate, string time) data)
        {
            var appointmentTime = TimeSpan.Parse(data.time);
            var dayOfWeek = data.appointmentDate.Date.DayOfWeek;

            //получаем время работы доктора для дня недели, на который записывается пациент
            if (data.appointmentDate.Doctor?.Schedule?.TryGetValue(dayOfWeek, out var workingHours) == true)
            {
                // проверяем, есть ли время, на которое хочет записаться пациент, в графике работы доктора
                return workingHours.Any(x => x == appointmentTime);
            }
            return false;
        }
    }
}
