using Database;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class DeleteGameHandler : AbstractHandler
{
    public DeleteGameHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message is "Закончить игру";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        try
        {
            await DeleteGame(update);
            await update.Messenger.SendMessage(
                    update.Chat,
                    update.Messenger.IsSupportMarkdown ? "*Игра закончена\\.*" : "Игра закончена.",
                    keyboardFactory.GetStartKeyboard(),
                    update.Messenger.IsSupportMarkdown
                );
        }
        catch (InvalidOperationException)
        {
            await update.Messenger.SendMessage(update.Chat, "Игра не найдена", keyboardFactory.GetStartKeyboard());
        }
    }

    private static async Task DeleteGame(Update update)
    {
        await using var context = new SlapBotDal();
        var game = await context.Games
            .Include(g => g.Slaps)
            .Include(g => g.Users)
            .FirstAsync(g => g.Id == update.Chat.Id);
        context.Games.Remove(game);
        await context.SaveChangesAsync();
    }
}