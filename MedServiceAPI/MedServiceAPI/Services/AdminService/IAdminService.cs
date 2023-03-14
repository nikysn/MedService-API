using MedServiceAPI.Model;

namespace MedServiceAPI.Services.AdminService
{
    public interface IAdminService
    {
        Task<Doctor> AddDoctor();
    }
}
