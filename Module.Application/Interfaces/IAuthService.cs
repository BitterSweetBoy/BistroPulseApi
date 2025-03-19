using Module.Application.DTOs;

namespace Module.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto model);
        Task<bool> LoginAsync(LoginDto model);
        Task<bool> LogoutAsync();
    }
}
