namespace CalmIsTarry.Auth.Repositories;

using CalmIsTarry.Models;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByUserIdAsync(int userId);
    Task<User?> GetUserByUserNameAsync(string username);
    Task<IEnumerable<string>> GetUserRolesAsync(int userId);
    Task<bool> AddUserAsync(User user);
}