using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

public class User
{
    [Key]
    public int Id { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
    
    public bool IsSuperAdmin { get; set; } = true;

    public void PerformAdminTask()
    {
        if (IsSuperAdmin)
        {
            // Perform task
        }
        else
        {
            throw new UnauthorizedAccessException("Not an admin.");
        }
    }
    public void SetPasswordHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            PasswordSalt = Convert.ToBase64String(hmac.Key);
            PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }

    public bool VerifyPassword(string password)
    {
        using (var hmac = new HMACSHA512(Convert.FromBase64String(PasswordSalt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHashBase64 = Convert.ToBase64String(computedHash);

            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(PasswordHash),
                Convert.FromBase64String(computedHashBase64));
        }
    }
}