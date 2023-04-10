using MedService.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Interfaces
{
    public interface IAdminRepository : IBaseRepository
    {
        Task<int> GetAdminsCountAsync();
        Task AddAdminAsync(Admin admin);
        Task<Admin> GetAdminByLoginAsync(string login);
    }
}
