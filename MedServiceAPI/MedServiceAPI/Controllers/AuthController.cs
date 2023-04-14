using MedService.DAL.DTO;
using MedServiceAPI.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Registration")]

        public async Task<ActionResult> Registration(NewUserDto newUser)
        {
            await _authService.Registration(newUser);
            return Ok();
        }

        [HttpPost("DoctorRegistration"), Authorize(Roles = "Admin")]

        public async Task<ActionResult> DoctorRegistration(NewDoctor newDoctor)
        {
            await _authService.DoctorRegistration(newDoctor);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string login, string password)
        {
            var token = await _authService.Login(login, password);
            return Ok(token);
        }
    }
}
