using System.Text.Json;

namespace OcelotApiGw.Middleware;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; init; }=string.Empty;
    public string ErrorMessage { get; init; }=String.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}