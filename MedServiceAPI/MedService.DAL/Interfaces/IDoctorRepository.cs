using MedService.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        Task AddDoctorAsync(Doctor doctor);
        Task<Doctor> GetDoctor(int id);
        Task<List<Doctor>> GetAllDoctors();
        Task<List<Doctor>> DeleteDoctor(Doctor doctor);
        Task<Doctor> GetDoctorByLastName(string lastName);
    }
}
