using MedService.Contracts.Abstraction.Repositories;
using MedService.DAL.Data;
using MedService.DAL.Model;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Patient> GetUserByLoginAsync(string userLogin)
        {
            var patient = await _dataContext.Patients.FirstOrDefaultAsync(u => u.Login == userLogin);
            return patient;
        }

        public async Task SaveChanges()
        {
            _dataContext.SaveChanges();
        }
    }
}
