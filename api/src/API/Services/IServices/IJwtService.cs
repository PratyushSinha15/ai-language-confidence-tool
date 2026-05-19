using API.Entity;

namespace API.Services.IServices;

public interface IJwtService
{
    string GenerateToken(User user);
}