using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.Abstraction.Repositories
{
    public interface IAdminRepository
    {
        Task<int> GetAdminsCountAsync();
        Task AddAdminAsync(Admin admin);
        Task<Admin> GetAdminByLoginAsync(string login);
    }
}
