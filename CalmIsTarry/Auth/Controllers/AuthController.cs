using CalmIsTarry.Auth.Services;
using CalmIsTarry.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CalmIsTarry.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var (success, user) = await _authService.AuthenticateUser(request.Username, request.Password);
        if (success)
        {
            return Ok(new { Message = "Login successful", Username = user.Username, Email = user.Email });
        }

        return Unauthorized("Invalid username or password");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var result = await _authService.RegisterUser(request.Username, request.Email, request.Password);

        return result ? Ok(new { Message = "Register successful" }) : BadRequest("Username or email already exists");
    }
    
    
}