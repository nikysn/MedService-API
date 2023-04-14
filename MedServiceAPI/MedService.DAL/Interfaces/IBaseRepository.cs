using MedService.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task SaveChanges();
        Task<T> GetUserByLoginAsync(string login);
    }
}
