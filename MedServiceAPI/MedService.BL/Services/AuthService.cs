using MedService.Contracts.Requests.User.Doctor;
using MedService.Contracts.Requests.User;
using MedService.DAL.Abstraction.Repositories;
using MedService.DAL.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using MedService.Contracts.Abstraction.Services;

namespace MedService.BL.Services
{
    public class AuthService : IAuthService
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";

        private readonly IConfiguration _configuration;
        private readonly IAdminRepository _adminRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService
            (
            IConfiguration configuration,
            IAdminRepository adminRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _configuration = configuration;
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Registration(CreateUserRequest createUserRequest)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(createUserRequest.Password);

            int adminCount = await _adminRepository.GetAdminsCountAsync();
            string role = adminCount == 0 ? Admin : Patient;

            if (role == Admin)
            {
                Admin admin = new Admin();
                admin.Login = createUserRequest.Login;
                admin.PasswordHash = passwordHash;
                admin.FirstName = createUserRequest.FirstName;
                admin.LastName = createUserRequest.LastName;
                admin.Role = role;

                var existingAdmin = await _adminRepository.GetUserByLoginAsync(admin.Login);
                if (existingAdmin != null)
                {
                    throw new ArgumentException("Пользователь с таким логином уже существует");
                }
                await _adminRepository.AddAdminAsync(admin);
            }
            if (role == Patient)
            {
                Patient patient = new Patient();
                patient.FirstName = createUserRequest.FirstName;
                patient.LastName = createUserRequest.LastName;
                patient.Role = role;
                patient.Login = createUserRequest.Login;
                patient.PasswordHash = passwordHash;

                var existingPatient = await _patientRepository.GetUserByLoginAsync(patient.Login);
                if (existingPatient != null)
                {
                    throw new ArgumentException("Пользователь с таким логином уже существует");
                }

                await _patientRepository.AddPatientAsync(patient);
            }

            await _adminRepository.SaveChanges();
        }

        public async Task<string> Login(SignInRequest signInRequest)
        {
            var admin = await _adminRepository.GetAdminByLoginAsync(signInRequest.Login);
            if (admin != null && BCrypt.Net.BCrypt.Verify(signInRequest.Password, admin.PasswordHash))
            {
                string token = CreateToken(admin);
                return token;
            }

            var patient = await _patientRepository.GetUserByLoginAsync(signInRequest.Login);
            if (patient != null && BCrypt.Net.BCrypt.Verify(signInRequest.Password, patient.PasswordHash))
            {
                string token = CreateToken(patient);
                return token;
            }

            var doctor = await _doctorRepository.GetUserByLoginAsync(signInRequest.Login);
            if (doctor != null && BCrypt.Net.BCrypt.Verify(signInRequest.Password, doctor.PasswordHash))
            {
                string token = CreateToken(doctor);
                return token;
            }
            else
                throw new NotImplementedException("Неправильный логин или пароль.");
        }

        public async Task Logout()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                
            }
        }

        public async Task DoctorRegistration(CreateDoctorRequest createDoctorRequest)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(createDoctorRequest.Password);

            Doctor doctor = new Doctor(createDoctorRequest.FirstName, createDoctorRequest.LastName, createDoctorRequest.Speciality);
            doctor.Login = createDoctorRequest.Login;
            doctor.PasswordHash = passwordHash;
            doctor.Role = Doctor;

            var existingDoctor = await _doctorRepository.GetUserByLoginAsync(doctor.Login);
            if (existingDoctor != null)
            {
                throw new ArgumentException("Пользователь с таким логином уже существует");
            }

            await _doctorRepository.AddDoctorAsync(doctor);
            await _doctorRepository.SaveChanges();
        }

        public async Task<Patient> GetCurrentPatient()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

                if (role != Patient)
                {
                    throw new InvalidOperationException("Текущий пользователь не является пациентом");
                }

                var patient = await _patientRepository.GetUserByLoginAsync(userName);
                if (patient == null)
                {
                    throw new InvalidOperationException($"Пациент с именем пользователя '{userName}' не найден.");
                }
                return patient;
            }
            return null;
        }

        private string CreateToken(dynamic user)
        {
            string role = user.Role;

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Login),
                new Claim(ClaimTypes.Role, role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
