using Telegram.Bot;

namespace API.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddTelegramSingleton(this IServiceCollection serviceCollection, string secret) =>
        serviceCollection.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(secret));
}