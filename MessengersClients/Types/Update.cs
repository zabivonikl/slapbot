using MessengersClients.ClientAdapters;

namespace MessengersClients.Types;

public class Update
{
    public Update(Messenger messenger, Chat chat, User user, string? message)
    {
        Messenger = messenger;
        Chat = chat;
        User = user;
        Message = message;
    }

    public Messenger Messenger { get; }

    public Chat Chat { get; }

    public User User { get; }

    public string? Message { get; }
}