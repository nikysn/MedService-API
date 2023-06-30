using FluentValidation;
using MedService.Contracts.Abstraction.Repositories;
using MedService.Contracts.DTOModel.Appointment;

namespace MedService.BL.Validation
{
    public class AppointmentDateRequestValidator : AbstractValidator<AppointmentDto>
    {
        private readonly IWorkingHoursRepository _workingHoursRepository;
        public AppointmentDateRequestValidator(IWorkingHoursRepository workingHoursRepository)
        {
            _workingHoursRepository = workingHoursRepository;

            RuleFor(x => x.Date).NotEmpty().WithMessage("Дата не может быть пустой");
            RuleFor(x => x).Must(BeWithin7Days).WithMessage("Запись к врачу только на 7 дней вперед");
            RuleFor(x => x).MustAsync(BeWeekDay).WithMessage("В эту дату у доктора выходной");  //Если не будет работать то нужно как то использовать cancellationToken.None
        }

        private bool BeWithin7Days(AppointmentDto appointmentDto)
        {
            return appointmentDto.Date > DateTime.Now.Date && appointmentDto.Date <= DateTime.Now.AddDays(7).Date;
        }

        private async Task<bool> BeWeekDay(AppointmentDto appointmentDto, CancellationToken cancellationToken)
        {
            var workingHourse = await _workingHoursRepository.GetWorkingHours(appointmentDto);
            bool asd =  workingHourse.Any(wh => wh.Schedule.ContainsKey(appointmentDto.Date.DayOfWeek));
            return asd;
        }
    }
}