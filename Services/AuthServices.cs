using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

public class AuthService{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ApplicationDbContext context , ILogger<AuthService> logger)
    {
        _context = context;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

     public bool Authenticate(string email, string password)
    {
        var admin = _context.Admins.SingleOrDefault(u => u.Email == email);
        if (admin == null)
        {
            return false;
        }
        return admin.VerifyPassword(password);
    }
}