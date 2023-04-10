using MedService.DAL.DTO;

namespace MedServiceAPI.Services.AdminService
{
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Registration(NewUserDto request);

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Login(string login, string password);

        /// <summary>
        /// Регистрация доктора
        /// </summary>
        /// <param name="newDoctor"></param>
        /// <returns></returns>
        Task DoctorRegistration(NewDoctor newDoctor);
    }
}
