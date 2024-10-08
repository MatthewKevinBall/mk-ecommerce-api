using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;

public class AuthService
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ApplicationDbContext context, ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    public UserResponse Authenticate(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email);
        if (user == null || !user.VerifyPassword(password))
        {
            return null;
        }

        return ConversionHelper.UserToUserResponse(user);
    }
}
