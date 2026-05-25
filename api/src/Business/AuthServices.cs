using Repository.DTOs.Auth;
using Repository.IRepository;
using API.Services.IServices;
using Business.IServices;
using Repository.Models;

namespace Business;

public class AuthServices : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthServices(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
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
        var token= _jwtService.GenerateToken(user);

        var AuthResponse = new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token
        };
        return AuthResponse;
    }
    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isValid)
        {
            throw new Exception("Invalid Username or password");
        }
        var token=_jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token
        };
    }
    
    public async Task<AuthResponseDto> GetMeAsync(string userId)
    {
        var user= await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return new AuthResponseDto
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = string.Empty
        };
    }
}