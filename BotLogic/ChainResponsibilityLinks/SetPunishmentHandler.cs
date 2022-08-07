using Database;
using MessengersClients;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class SetPunishmentHandler : AbstractHandler
{
    public SetPunishmentHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update)
    {
        using var context = new SlapBotDal();
        return context.Games.Any(g => g.Id == update.Chat.Id && g.Punishment == null);
    }

    protected override async Task _Handle(Update update)
    {
        base._Handle(update);
        await using (var context = new SlapBotDal())
        {
            var game = await context.Games.FirstAsync(g => g.Id == update.Chat.Id);
            game.Punishment = update.Message;
            context.SaveChangesAsync();
        }

        update.Messenger.SendMessage(
                update.Chat,
                GetResponse(update),
                keyboardFactory.GetStartKeyboard(),
                update.Messenger.IsSupportMarkdown
            );
    }

    private static string GetResponse(Update update) =>
        update.Messenger.IsSupportMarkdown
            ? $"Установлено наказание: _{update.Message?.ToLower().EscapeSymbols() ?? "Не указано"}_"
            : $"Установлено наказание: {update.Message?.ToLower() ?? "Не указано"}";
}