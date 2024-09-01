using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(ApplicationDbContext context, ILogger<RegisterController> logger)
    {
        _context = context;
        _logger = logger;

    }

    [HttpPost("add/user")]
    public async Task<IActionResult> AddAdmin([FromBody] LoginRequestDto user)
    {
        if (user == null)
        {
            return BadRequest("User data is null.");
        }

        if (!await IsEmailUniqueAsync(user.Email))
        {
            return BadRequest("Email already exisits");

        }
        User newAdmin = createNewAdminObjectFromLoginRequest(user);
        await addAdminToBackEnd(newAdmin);

        return Ok("Success!!");
    }

    private async Task addAdminToBackEnd(User newUser)
    {
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    private User createNewAdminObjectFromLoginRequest(LoginRequestDto user)
    {
        User newAdmin = new User
        {
            Email = user.Email,
        };
        newAdmin.SetPasswordHash(user.Password);
        return newAdmin;
    }

    private async Task<bool> IsEmailUniqueAsync(string email)
    {
        var existingAdmin = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
        return existingAdmin == null;
    }
}
