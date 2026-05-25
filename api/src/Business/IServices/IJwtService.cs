using Repository.Models;

namespace Business.IServices;

public interface IJwtService
{
    string GenerateToken(User user);
}