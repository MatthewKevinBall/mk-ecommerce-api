using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

public abstract class UserBase
{
    [Key]
    public int Id { get; set; } // Primary key

    [EmailAddress]
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PasswordSalt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

   public void SetPasswordHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            PasswordSalt = Convert.ToBase64String(hmac.Key); // Store the salt
            PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))); // Store the hashed password
        }
    }

   public bool VerifyPassword(string password)
    {
        using (var hmac = new HMACSHA512(Convert.FromBase64String(PasswordSalt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHashBase64 = Convert.ToBase64String(computedHash);
            
            // Use a constant-time comparison
            return CryptographicOperations.FixedTimeEquals(
                Convert.FromBase64String(PasswordHash),
                Convert.FromBase64String(computedHashBase64));
        }
    }
}
