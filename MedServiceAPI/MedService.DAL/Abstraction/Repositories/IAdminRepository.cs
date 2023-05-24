using MedService.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Abstraction.Repositories
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<int> GetAdminsCountAsync();
        Task AddAdminAsync(Admin admin);
        Task<Admin> GetAdminByLoginAsync(string login);
    }
}
