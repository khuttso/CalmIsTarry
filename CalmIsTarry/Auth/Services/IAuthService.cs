namespace CalmIsTarry.Auth.Services;

using CalmIsTarry.Models;

public interface IAuthService
{
    public Task<(bool Success, User user)> AuthenticateUser(string username, string password);
    public Task<string> HashPassword(string password);
    public Task<bool> RegisterUser(string username, string email, string password);
}   