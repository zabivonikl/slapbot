using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Chat = MessengersClients.Types.Chat;

namespace MessengersClients.ClientAdapters;

public class TelegramAdapter : IMessenger
{
    private readonly ITelegramBotClient client;

    public TelegramAdapter(ITelegramBotClient client) => this.client = client;

    public bool SupportMarkdown => true;

    public void SetTyping(Chat chat) => Task.Factory.StartNew(
            async () => await client.SendChatActionAsync(chat.ChatId, ChatAction.Typing)
        );

    public void SendMessage(Chat chat, string text) => Task.Factory.StartNew(
            async () => await client.SendTextMessageAsync(
                    chat.Username is null ? new ChatId(chat.ChatId) : new ChatId(chat.Username),
                    text
                )
        );

    public void SendMarkdownMessage(Chat chat, string text) => Task.Factory.StartNew(
            async () => await client.SendTextMessageAsync(
                    chat.Username is null ? new ChatId(chat.ChatId) : new ChatId(chat.Username),
                    text,
                    ParseMode.MarkdownV2
                )
        );
}