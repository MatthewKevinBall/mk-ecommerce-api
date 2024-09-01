using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase 
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RegisterController> _logger;

    public UserController(ApplicationDbContext context, ILogger<RegisterController> logger)
    {
        _context = context;
        _logger = logger;

    }

     [HttpPut("{id}/update")]
     public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
         if (dto == null)
        {
            return BadRequest("DTO is null. Please check the JSON payload.");
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound(); // Add this check to avoid a null reference if the user isn't found.
        }
       
        if (!string.IsNullOrEmpty(dto.Email))
        {
            user.Email = dto.Email;
        }

        if (!string.IsNullOrEmpty(dto.FirstName))
        {
            user.FirstName = dto.FirstName;
        }

        if (!string.IsNullOrEmpty(dto.LastName))
        {
            user.LastName = dto.LastName;
        }

        if (!string.IsNullOrEmpty(dto.PhoneNumber))
        {
            user.PhoneNumber = dto.PhoneNumber;
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return NoContent();

    }

}