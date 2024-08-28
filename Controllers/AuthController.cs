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
        UserResponse userResponse = _authService.Authenticate(user.Email, user.Password);
        if (userResponse != null)
        {
            var response = new RequestResponse
            {
                name = "Success", // Property names should be in PascalCase
                result = userResponse.ToJson() // Set `Result` to the actual response object
            };
            return Ok(response);
        }
        var failure = new RequestResponse
        {
            name = "Failure", // Property names should be in PascalCase
            result = "Invalid Credentials" // Set `Result` to the actual response object
        };

        return Unauthorized(failure);
    }
}
