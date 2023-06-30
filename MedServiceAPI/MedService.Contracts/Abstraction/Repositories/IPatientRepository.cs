using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.Abstraction.Repositories
{
    public interface IPatientRepository
    {
        Task AddPatientAsync(Patient patient);
        // Task<Patient> GetPatientByLoginAsync(string login);
    }
}
