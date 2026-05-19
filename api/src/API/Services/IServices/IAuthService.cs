using API.DTOs.Auth;

namespace API.Services.IServices;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    
    Task<AuthResponseDto> GetMeAsync(string userId);
}