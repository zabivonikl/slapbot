using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;

namespace MessengersClients.ClientAdapters;

public abstract class Messenger
{
    public virtual bool IsSupportMarkdown => false;

    public abstract Task SetTyping(Chat chat);

    public abstract Task SendMessage(Chat chat, string text, Keyboard kb, bool isMarkdown = false);
}