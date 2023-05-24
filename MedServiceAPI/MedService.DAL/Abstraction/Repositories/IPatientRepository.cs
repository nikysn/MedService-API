using MedService.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Abstraction.Repositories
{
    public interface IPatientRepository : IBaseRepository<Patient>
    {
        Task AddPatientAsync(Patient patient);
        // Task<Patient> GetPatientByLoginAsync(string login);
    }
}
