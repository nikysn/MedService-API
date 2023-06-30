using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.DTOModel.User.Doctor
{
    public class WorkingHoursDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Dictionary<DayOfWeek, List<TimeSpan>> Schedule { get; set; }
      //  public DayOfWeek DayOfWeek { get; set; }
      //  public TimeSpan AppointmentTime { get; set; }
    }
}
