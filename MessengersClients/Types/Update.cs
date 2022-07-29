using MessengersClients.ClientAdapters;

namespace MessengersClients.Types;

public class Update
{
    public Update(IMessenger messenger, Chat chat, string? message)
    {
        Messenger = messenger;
        Chat = chat;
        Message = message;
    }

    public IMessenger Messenger { get; }

    public Chat Chat { get; }

    public string? Message { get; }
}