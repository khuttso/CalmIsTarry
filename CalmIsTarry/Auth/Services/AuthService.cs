using System.Security.Cryptography;
using System.Text;
using CalmIsTarry.Auth.Repositories;
using CalmIsTarry.Models;

namespace CalmIsTarry.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<(bool Success, User user)> AuthenticateUser(string username, string password)
    {
        var user = await _userRepository.GetUserByUserNameAsync(username);
        if (user == null)
        {
            return (false, new User() {UserId = -1});
        }

        string hashedPassword = await HashPassword(password);
        return (hashedPassword == user.Password, user);
    }

    public async Task<string> HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    public async Task<bool> RegisterUser(string username, string email, string password)
    {
        var result = await _userRepository.AddUserAsync(new User()
            { Username = username, Email = email, Password = password });
        
        return result;
    }
}