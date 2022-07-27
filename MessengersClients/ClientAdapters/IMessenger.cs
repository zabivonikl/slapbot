using MessengersClients.Types;

namespace MessengersClients.ClientAdapters;

public interface IMessenger
{
    bool SupportMarkdown { get; }

    void SetTyping(Chat chat);

    void SendMessage(Chat chat, string text);

    void SendMarkdownMessage(Chat chat, string text);
}