using MedService.DAL.Abstraction.Repositories;
using MedService.DAL.Data;
using MedService.DAL.Model;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Admin> GetAdminByLoginAsync(string adminLogin)
        {
            var admin = await _dataContext.Admins.SingleOrDefaultAsync(x => x.Login == adminLogin);
            return admin;
        }

        public async Task<int> GetAdminsCountAsync()
        {
            int adminCount = await _dataContext.Admins.CountAsync();
            return adminCount;
        }

        public async Task<Admin> GetUserByLoginAsync(string userLogin)
        {
            var admin = await _dataContext.Admins.FirstOrDefaultAsync(u => u.Login == userLogin);
            return admin;
        }

        public async Task SaveChanges()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
