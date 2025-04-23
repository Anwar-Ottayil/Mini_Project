using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Dto;
using MiniProject.Services.LoginService;

namespace MiniProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            var token = await _loginService.Login(loginDto);
            if (token == null)
            {
                return NotFound("user not found");
            }
            return Ok(token);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                bool newuser = await _loginService.UserRegistration(registerDto);
                if (!newuser)
                {
                    return BadRequest("User already exist");
                }
                return Ok("user created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new Exception(ex.Message));
            }
        }
    }
}
