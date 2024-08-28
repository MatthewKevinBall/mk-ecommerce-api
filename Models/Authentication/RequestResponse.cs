using System.Text.Json;

public abstract class RequestResponseBase
{
    public string? name { set; get; }
    public string? result { set; get; }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class RequestResponse : RequestResponseBase
{

}
