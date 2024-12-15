using System.Data;
using CalmIsTarry.Models;
using Dapper;

namespace CalmIsTarry.Auth.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly int _commandTimeOut = 60;

    public UserRepository(IDbConnection connection)
    {
        _dbConnection = connection;
    }
    
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var cmdParams = new DynamicParameters(new {});
        var cmd = new CommandDefinition(
            "System_GetAllUsers",
            cmdParams,
            commandType: CommandType.StoredProcedure,
            commandTimeout: _commandTimeOut
        );

        var users = await _dbConnection.QueryAsync<User>(cmd);

        return users;
    }

    public async Task<User?> GetUserByUserIdAsync(int userId)
    {
        var cmdParams = new DynamicParameters(new {UserID = userId});
        var cmd = new CommandDefinition(
            "System_GetUserByUserID",
            cmdParams,
            commandType: CommandType.StoredProcedure,
            commandTimeout: _commandTimeOut
            );

        var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(cmd);

        return user;
    }

    public async Task<User?> GetUserByUserNameAsync(string username)
    {
        var cmdParams = new DynamicParameters(new {Username = username});
        var cmd = new CommandDefinition(
            "System_GetUserByUsername",
            cmdParams,
            commandType: CommandType.StoredProcedure,
            commandTimeout: _commandTimeOut
        );

        var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(cmd);

        return user;
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
    {
        var cmdParams = new DynamicParameters(new {UserID = userId});
        var cmd = new CommandDefinition(
            "System_GetUserRoles",
            cmdParams,
            commandType: CommandType.StoredProcedure,
            commandTimeout: _commandTimeOut
        );

        var roles = await _dbConnection.QueryAsync<string>(cmd);
    
        return roles;
    }

    public async Task<bool> AddUserAsync(User user)
    {
        var cmdParams = new DynamicParameters(new
            { Username = user.Username, Email = user.Email, Password = user.Password });

        var cmd = new CommandDefinition(
            "System_AddUser",
            cmdParams,
            commandType: CommandType.StoredProcedure,
            commandTimeout: _commandTimeOut
            );

        try
        {
            var rowsAffected = await _dbConnection.ExecuteAsync(cmd);

            return rowsAffected == 1;
        }
        catch (Exception e)
        {
            // TODO - Logging error message
            return false;
        }
    }
}