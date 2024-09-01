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
    public IActionResult Login([FromBody] LoginRequestDto user)
    {
        UserResponse userResponse = _authService.Authenticate(user.Email, user.Password);
        if (userResponse != null)
        {
            var response = new RequestResponse
            {
                name = "Success", 
                result = userResponse.ToJson() // 
            };
            return Ok(response);
        }
        var failure = new RequestResponse
            {
                name = "Failure", 
                result = "Invalid Credentials" 
            };

        return Unauthorized(failure);
    }
}