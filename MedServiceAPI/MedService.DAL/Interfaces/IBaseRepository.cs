using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.DAL.Interfaces
{
    public interface IBaseRepository
    {
        Task SaveChanges();
    }
}
