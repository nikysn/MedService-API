using MedService.Contracts.Abstraction.Services;
using MedServiceAPI.Model.Requests.User;
using MedServiceAPI.Model.Requests.User.Doctor;
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

        public async Task<ActionResult> Registration([FromBody]CreateUserRequest createUserRequest)
        {
            await _authService.Registration(createUserRequest);
            return Ok();
        }

        [HttpPost("DoctorRegistration"), Authorize(Roles = "Admin")]

        public async Task<ActionResult> DoctorRegistration([FromBody] CreateDoctorRequest createDoctorRequest)
        {
            await _authService.DoctorRegistration(createDoctorRequest);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] SignInRequest signInRequest)
        {
            var token = await _authService.Login(signInRequest);
            return Ok(token);
        }
    }
}
