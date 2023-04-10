using MedService.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Interfaces
{
    public interface IPatientRepository : IBaseRepository
    {
        Task AddPatientAsync(Patient patient);
        Task<Patient> GetPatientByLoginAsync(string login);
    }
}
