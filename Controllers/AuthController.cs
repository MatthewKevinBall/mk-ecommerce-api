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
    public IActionResult Login([FromBody] Admin user)
    {
        _logger.LogDebug("Attempting to log in with username: {Username}", user.Username);

        if (_authService.Authenticate(user.Username, user.Password))
        {
            _logger.LogInformation("Login successful for username: {Username}", user.Username);
            return Ok("Login successful");
        }

        _logger.LogWarning("Login failed for username: {Username}", user.Username);
        return Unauthorized("Invalid credentials");
    }
}