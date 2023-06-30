using MedService.Contracts.Requests.User.Doctor;
using MedService.Contracts.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedService.Contracts.Abstraction.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Registration(CreateUserRequest createUserRequest);

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Login(SignInRequest signInRequest);

        /// <summary>
        /// Регистрация доктора
        /// </summary>
        /// <param name="newDoctor"></param>
        /// <returns></returns>
        Task DoctorRegistration(CreateDoctorRequest createDoctorRequest);
        /// <summary>
        /// Получение Id юзера который залогинелся
        /// </summary>
        /// <returns></returns>
        Task<Patient> GetCurrentPatient();     // дто
    }
}
