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
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _dataContext;

        public AdminRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAdminAsync(Admin admin)
        {
            _dataContext.Admins.Add(admin);
        }

        public async Task<Admin> GetAdminByLoginAsync(string login)
        {
            var admin = await _dataContext.Admins.SingleOrDefaultAsync(x => x.Login == login);
            return admin;
        }

        public async Task<int> GetAdminsCountAsync()
        {
            int adminCount = await _dataContext.Admins.CountAsync();
            return adminCount;
        }

        public async Task<Admin> GetUserByLoginAsync(string login)
        {
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(u => u.Login == login);
            return admin;
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
