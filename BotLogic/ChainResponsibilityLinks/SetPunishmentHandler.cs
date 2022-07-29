using Database;
using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class SetPunishmentHandler : AbstractHandler
{
    public SetPunishmentHandler(IKeyboard kb, AbstractHandler? next = null) : base(kb, next)
    {
    }

    protected override bool CanHandle(Update update)
    {
        using var context = new SlapBotDal();
        return context.Games
            .Include(g => g.Users)
            .Any(g => g.Users.Contains(update.Chat) && g.Punishment == null);
    }

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        IEnumerable<Chat> chats;
        await using (var context = new SlapBotDal())
        {
            var game = await context.Games
                .Include(g => g.Users)
                .Where(g => g.Users.Contains(update.Chat))
                .FirstAsync();
            game.Punishment = update.Message;
            chats = game.Users.ToList();
            await context.SaveChangesAsync();
        }

        foreach (var chat in chats)
            await update.Messenger.SendMessage(chat, GetResponse(update), kb, update.Messenger.IsSupportMarkdown);
    }

    private static string GetResponse(Update update) =>
        update.Messenger.IsSupportMarkdown ? $"Установлено наказание: _{update.Message?.ToLower() ?? "Не указано"}_"
            : $"Установлено наказание: {update.Message?.ToLower() ?? "Не указано"}";
}