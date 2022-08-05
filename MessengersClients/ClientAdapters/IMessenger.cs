using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;

namespace MessengersClients.ClientAdapters;

public interface IMessenger
{
    bool IsSupportMarkdown { get; }

    Task SetTyping(Chat chat);

    Task SendMessage(Chat chat, string text, IKeyboard kb, bool isMarkdown = false);
}