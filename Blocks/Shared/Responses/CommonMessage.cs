namespace Shared.Responses;

public class CommonMessage
{
    public CommonMessage(int id, string message)
    {
        Id = id;
        Message = message;
    }

    public string Message { get; init; }
    public int Id { get; init; }
}