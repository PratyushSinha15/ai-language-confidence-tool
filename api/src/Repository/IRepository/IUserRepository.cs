using Repository.Models;

namespace Repository.IRepository;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(String userId);
    Task<User?> GetUserByUsernameAsync(string username);
    Task AddAsync(User user);
}