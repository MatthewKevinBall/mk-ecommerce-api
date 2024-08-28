using System.Text.Json;

public class UserResponse
{
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Id { get; set; }

    public string PhoneNumber { get; set; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

}
