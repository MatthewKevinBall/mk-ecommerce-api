using System.ComponentModel.DataAnnotations;
public abstract class UserBase
{
    [Key]
    public int Id { get; set; } // Primary key

    public string Username { get; set; }
    public string Password { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}
