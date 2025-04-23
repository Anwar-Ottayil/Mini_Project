using MiniProject.Dto;

namespace MiniProject.Services.LoginService
{
    public interface ILoginService
    {
        Task<bool> UserRegistration(RegisterDto registerDto);
        Task<ResponseDto> Login(LoginDto loginDto);
    }
}
