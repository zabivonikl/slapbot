namespace MessengersClients.Types;

public class Chat
{
    public Chat(long id, string? title = null)
    {
        Id = id;
        Title = title;
    }

    public long Id { get; init; }

    public string? Title { get; init; }
}