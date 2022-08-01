using MessengersClients.ClientAdapters;
using MessengersClients.Types;
using Telegram.Bot;

namespace MessengersClients;

public static class TelegramExtensions
{
    private static TelegramAdapter GetAdapter(this ITelegramBotClient client) => new(client);

    private static Chat GetAdapter(this Telegram.Bot.Types.Chat chat) =>
        new(chat.Id, chat.Title);

    private static User GetAdapter(this Telegram.Bot.Types.User user) =>
        new(user.Id, user.FirstName);

    public static Update GetAdapter(this Telegram.Bot.Types.Update update, ITelegramBotClient client) =>
        new(
                client.GetAdapter(),
                update.Message!.Chat.GetAdapter(),
                update.Message.From!.GetAdapter(),
                update.Message.Text!
            );
}