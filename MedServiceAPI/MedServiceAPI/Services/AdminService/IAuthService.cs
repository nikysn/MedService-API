using MedServiceAPI.Dto;
using MedServiceAPI.Model;

namespace MedServiceAPI.Services.AdminService
{
    public interface IAuthService
    {
        Task Registration(NewUserDto request);
        Task<string> Login(string login, string password);
        Task DoctorRegistration(NewDoctor newDoctor);
    }
}
