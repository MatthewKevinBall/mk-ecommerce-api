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

    [HttpPost("add/admin")]
    public async Task<IActionResult> AddAdmin([FromBody] LoginRequest user)
    {
        if (user == null)
        {
            return BadRequest("User data is null.");
        }

        if (!await IsEmailUniqueAsync(user.Email))
        {
            return BadRequest("Email already exisits");

        }
        Admin newAdmin = createNewAdminObjectFromLoginRequest(user);
        await addAdminToBackEnd(newAdmin);

        return Ok("Success!!");
    }

    private async Task addAdminToBackEnd(Admin newAdmin)
    {
        _context.Admins.Add(newAdmin);
        await _context.SaveChangesAsync();
    }

    private Admin createNewAdminObjectFromLoginRequest(LoginRequest user)
    {
        Admin newAdmin = new Admin
        {
            Email = user.Email,
        };
        newAdmin.SetPasswordHash(user.Password);
        return newAdmin;
    }

    private async Task<bool> IsEmailUniqueAsync(string email)
    {
        var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        return existingAdmin == null;
    }
}
