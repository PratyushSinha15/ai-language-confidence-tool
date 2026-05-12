using API.DTOs.Auth;

namespace API.Services.IServices;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
}