using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MessengersClients.ClientAdapters;

public class TelegramAdapter : Messenger
{
    private readonly ITelegramBotClient client;

    public TelegramAdapter(ITelegramBotClient client) => this.client = client;

    public override bool IsSupportMarkdown => true;

    public override async Task SetTyping(Chat chat) =>
        await client.SendChatActionAsync(
                chat.Id,
                ChatAction.Typing
            );

    public override async Task SendMessage(Chat chat, string text, Keyboard kb, bool isMarkdown = false) =>
        await client.SendTextMessageAsync(
                chat.Id,
                text,
                isMarkdown ? ParseMode.MarkdownV2 : null,
                replyMarkup: ((TelegramKeyboard)kb).GetKeyboard()
            );
}