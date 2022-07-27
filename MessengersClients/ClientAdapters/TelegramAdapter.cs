using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MessengersClients.ClientAdapters;

public class TelegramAdapter : IMessenger
{
    private readonly ITelegramBotClient client;

    public TelegramAdapter(ITelegramBotClient client) => this.client = client;

    public bool SupportMarkdown => true;

    public async Task SetTyping(Chat chat) =>
        await client.SendChatActionAsync(
                chat.Username is null ? chat.ChatId : chat.Username,
                ChatAction.Typing
            );

    public async Task SendMessage(Chat chat, string text, IKeyboard kb) =>
        await client.SendTextMessageAsync(
                chat.Username is null ? chat.ChatId : chat.Username,
                text,
                replyMarkup: ((TelegramKeyboard)kb).GetKeyboard()
            );

    public async Task SendMarkdownMessage(Chat chat, string text, IKeyboard kb) =>
        await client.SendTextMessageAsync(
                chat.Username is null ? chat.ChatId : chat.Username,
                text,
                ParseMode.MarkdownV2,
                replyMarkup: ((TelegramKeyboard)kb).GetKeyboard()
            );
}