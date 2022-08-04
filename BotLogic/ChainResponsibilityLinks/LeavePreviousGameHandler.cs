using Database;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class LeavePreviousGameHandler : AbstractHandler
{
    public LeavePreviousGameHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message == "Покинуть предыдущие игры";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await RemoveUserAndGame(update);
        await update.Messenger.SendMessage(
                update.Chat,
                "Предыдущие игры покинуты",
                keyboardFactory.GetStartKeyboard()
            );
    }

    private static async Task RemoveUserAndGame(Update update)
    {
        await using var context = new SlapBotDal();
        var user = await context.Users
            .FirstAsync(u => u.Id == update.User.Id);
        var game = await context.Games
            .Include(g => g.Users)
            .FirstOrDefaultAsync(g => g.Users.Contains(user));
        if (game?.Users.Count <= 1)
            context.Games.Remove(game);
        else
            context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}