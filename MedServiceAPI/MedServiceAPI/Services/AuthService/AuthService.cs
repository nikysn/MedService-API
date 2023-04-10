using AutoMapper;
using Azure.Core;
//using MedServiceAPI.Data;
using MedService.DAL.Data;
using MedService.DAL.Model;
using MedService.DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MedService.DAL.Interfaces;
using MedService.DAL.Repositories;

namespace MedServiceAPI.Services.AdminService
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
        public AuthService(IConfiguration configuration,IAdminRepository adminRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _configuration = configuration;
            _adminRepository = adminRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task Registration(NewUserDto newUser)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            int adminCount = await _adminRepository.GetAdminsCountAsync();
            string role = adminCount == 0 ? Admin : Patient;

            if(role== Admin)
            {
                Admin admin = new Admin();
                admin.Login = newUser.Login;
                admin.PasswordHash = passwordHash;
                admin.FirstName = newUser.FirstName;
                admin.LastName = newUser.LastName;
                admin.Role = role;

                _adminRepository.AddAdminAsync(admin);
               // _dataContext.Admins.Add(admin);
            }
            if(role == Patient)
            {
                Patient patient = new Patient();
                patient.FirstName = newUser.FirstName;
                patient.LastName = newUser.LastName;
                patient.Role = role;
                patient.Login = newUser.Login;
                patient.PasswordHash = passwordHash;

                _patientRepository.AddPatientAsync(patient);
            }

            _adminRepository.SaveChanges();
        }
        
        public async Task<string> Login(string login, string password)
        {
            //   var admin = await _dataContext.Admins.SingleOrDefaultAsync(x => x.Login == login);
            var admin = await _adminRepository.GetAdminByLoginAsync(login);
            if(admin != null && BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                string token = CreateToken(admin);
                return token;
            }

            // var patient = await _dataContext.Patients.SingleOrDefaultAsync(x => x.Login == login);
            var patient = await _patientRepository.GetPatientByLoginAsync(login);
            if (patient != null && BCrypt.Net.BCrypt.Verify(password, patient.PasswordHash))
            {
                string token = CreateToken(patient);
                return token;
            }
            else
            throw new NotImplementedException("Неправильный логин или пароль.");
        }

        public async Task DoctorRegistration(NewDoctor newDoctor)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(newDoctor.Password);

            Doctor doctor = new Doctor(newDoctor.FirstName,newDoctor.LastName,newDoctor.Speciality);
            doctor.Login = newDoctor.Login;
            doctor.PasswordHash = passwordHash;
            doctor.Role = Doctor;

            await _doctorRepository.AddDoctorAsync(doctor);
            _doctorRepository.SaveChanges();
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
