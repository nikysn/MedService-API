using MedService.DAL.Abstraction.Repositories;
using MedService.DAL.Data;
using MedService.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _dataContext;
        public DoctorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

         public async Task AddDoctorAsync(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
        }

        public async Task<List<Doctor>> DeleteDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task <List<Doctor>> GetAllDoctors()
        {
            return await _dataContext.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public async Task <Doctor> GetDoctor(int doctorId)
        {
            var doctor = await _dataContext.Doctors
               .Include(d => d.AppointmentDate)
               .ThenInclude(ad => ad.AppointmentTimes)
               .FirstOrDefaultAsync(d => d.Id == doctorId);

            if (doctor == null)
            {
                throw new ArgumentException("Такого доктора нет");
            }

            return doctor;
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Doctor> GetUserByLoginAsync(string userLogin)
        {
            var doctor = await _dataContext.Doctors.FirstOrDefaultAsync(u => u.Login == userLogin);
           
            if (doctor == null)
            {
                throw new ArgumentException("Такого доктора нет");     //  ИСПРАВИТЬ - когда добавляем первого доктора - он выдаст налл
            }
            return doctor;
        }
    }
}
