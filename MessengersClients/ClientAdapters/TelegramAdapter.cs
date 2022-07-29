using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MessengersClients.ClientAdapters;

public class TelegramAdapter : IMessenger
{
    private readonly ITelegramBotClient client;

    public TelegramAdapter(ITelegramBotClient client) => this.client = client;

    public bool IsSupportMarkdown => true;

    public async Task SetTyping(Chat chat) =>
        await client.SendChatActionAsync(
                chat.Username is null ? chat.ChatId : $"@{chat.Username}",
                ChatAction.Typing
            );

    public async Task SendMessage(Chat chat, string text, IKeyboard kb, bool isMarkdown = false) =>
        await client.SendTextMessageAsync(
                chat.Username is null ? chat.ChatId : $"@{chat.Username}",
                text,
                isMarkdown ? ParseMode.MarkdownV2 : null,
                replyMarkup: ((TelegramKeyboard)kb).GetKeyboard()
            );
}