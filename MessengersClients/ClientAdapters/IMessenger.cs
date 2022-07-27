using MessengersClients.Types;

namespace MessengersClients.ClientAdapters;

public interface IMessenger
{
    bool SupportMarkdown { get; }

    Task SetTyping(Chat chat);

    Task SendMessage(Chat chat, string text);

    Task SendMarkdownMessage(Chat chat, string text);
}