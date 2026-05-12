using API.DTOs.Auth;
using API.Entity;
using API.Repository.IRepository;
using API.Services.IServices;

namespace API.Services;

public class AuthServices : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    //Implementation Class for IAuthService
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto req)
    {
        // check 1
        var exsistingUsername = await _userRepository.GetUserByUsernameAsync(req.Username);
        if (exsistingUsername != null)
        {
            throw new Exception("Username already exists");
        }
        
        //check 2
        var exsistingEmail= await _userRepository.GetUserByEmailAsync(req.Email);
        if (exsistingEmail != null)
        {
            throw new Exception("Email already exists");
        }
        
        //add passwordHashing
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(req.Password);

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Username = req.Username,
            Email = req.Email,
            PasswordHash = passwordHash,
            FirstName = req.FirstName,
            LastName = req.LastName,
            CreatedAt = DateTime.UtcNow
        };
        await _userRepository.AddAsync(user);

        var AuthResponse = new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = string.Empty
        };
        return AuthResponse;
    }
}