using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

     [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest user)
    {
        if (_authService.Authenticate(user.Email, user.Password))
        {

            return Ok("Login successful");
        }
        return Unauthorized("Invalid credentials");
    }
}