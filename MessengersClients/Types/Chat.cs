namespace MessengersClients.Types;

public class Chat
{
    public Chat(long chatId, string? username = null)
    {
        ChatId = chatId;
        Username = username;
    }

    public long ChatId { get; }

    public string? Username { get; }
}