using MedService.Contracts.DTOModel.Appointment;
using MedService.Contracts.DTOModel.User.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.Abstraction.Repositories
{
    public interface IWorkingHoursRepository
    {
        Task<IEnumerable<WorkingHoursDto>> GetWorkingHours(AppointmentDto appointmentDto);
    }
}
