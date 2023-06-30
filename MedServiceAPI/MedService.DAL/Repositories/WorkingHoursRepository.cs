using MapsterMapper;
using MedService.Contracts.Abstraction.Repositories;
using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;
using MedService.DAL.Data;
using MedService.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace MedService.DAL.Repositories
{
    public class WorkingHoursRepository : IWorkingHoursRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public WorkingHoursRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = new DataContext();
            _mapper = mapper;
        }
        public async Task<IEnumerable<WorkingHoursDto>> GetWorkingHours(AppointmentDto appointmentDto)
        {
            var workingHours = await _dataContext.WorkingHours
                .Where(wh => wh.DoctorId == appointmentDto.DoctorId && wh.DayOfWeek == (int)appointmentDto.Date.DayOfWeek)
                .ToArrayAsync();

            
            return _mapper.Map<WorkingHours[], WorkingHoursDto[]>(workingHours);


        }
    }
}
