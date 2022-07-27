using MessengersClients.ClientAdapters;
using MessengersClients.Types;
using Telegram.Bot;

namespace MessengersClients;

public static class TelegramExtensions
{
    private static TelegramAdapter GetAdapter(this ITelegramBotClient client) => new(client);

    private static Chat GetAdapter(this Telegram.Bot.Types.Chat chat) =>
        new(chat.Id /*, chat.Username is null ? chat.Username : "@" + chat.Username*/);

    public static Update GetAdapter(this Telegram.Bot.Types.Update update, ITelegramBotClient client) =>
        new(client.GetAdapter(), update.Message!.Chat.GetAdapter(), update.Message.Text!);
}