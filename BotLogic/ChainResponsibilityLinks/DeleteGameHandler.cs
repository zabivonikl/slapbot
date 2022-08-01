using Database;
using Database.Entities;
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
        Game game;
        await using (var context = new SlapBotDal())
        {
            game = await context.Games
                .Include(g => g.Slaps)
                .Include(g => g.Users)
                .Where(g => g.Id == update.Chat.Id)
                .FirstAsync();
            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }

        await update.Messenger.SendMessage(
                update.Chat,
                update.Messenger.IsSupportMarkdown ? game.GetMarkdownResult() : game.GetResult(),
                keyboardFactory.GetStartKeyboard(),
                update.Messenger.IsSupportMarkdown
            );
    }
}