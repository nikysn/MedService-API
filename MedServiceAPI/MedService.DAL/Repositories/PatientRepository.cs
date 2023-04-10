using MedService.DAL.Data;
using MedService.DAL.Interfaces;
using MedService.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _dataContext;
        public PatientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            _dataContext.Patients.Add(patient);
        }

        public async Task<Patient> GetPatientByLoginAsync(string login)
        {
            var patient = await _dataContext.Patients.SingleOrDefaultAsync(x => x.Login == login);
            return patient;
        }

        public async Task SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
