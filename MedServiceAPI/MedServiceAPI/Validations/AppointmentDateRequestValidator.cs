using FluentValidation;
using MedServiceAPI.Model;
using System.Numerics;

namespace MedServiceAPI.Validations
{
    public class AppointmentDateRequestValidator : AbstractValidator<(Doctor doctor,DateTime date)>
    {
        public AppointmentDateRequestValidator()
        {
            RuleFor(x => x.date).NotEmpty().WithMessage("Дата не может быть пустой");
            RuleFor(x => x.date).Must(BeWithin7Days).WithMessage("Запись к врачу только на 7 дней вперед");
            RuleFor(x => x).Must(BeWeekDay).WithMessage("На эту дату доктор не работает");
        }

        private bool BeWithin7Days(DateTime date)
        {
            return date >= DateTime.Now.Date && date <= DateTime.Now.AddDays(7).Date;
        }

        private bool BeWeekDay((Doctor doctor,DateTime date) data)
        {
            return data.doctor.Schedule.ContainsKey(data.date.DayOfWeek);
        }
    }
}
