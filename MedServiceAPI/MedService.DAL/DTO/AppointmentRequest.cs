using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.DTO
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}
