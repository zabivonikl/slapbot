using Database;
using Database.Entities;
using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class DeleteGameHandler : AbstractHandler
{
    public DeleteGameHandler(IKeyboard kb, AbstractHandler? next = null) : base(kb, next)
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
                .Include(g => g.Users)
                .Where(g => g.Users.Contains(update.Chat))
                .FirstAsync();
            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }
        
        await update.Messenger.SendMessage(
            update.Chat, 
            update.Messenger.IsSupportMarkdown ? game.GetMarkdownResult() : game.GetResult(), 
            kb,
            update.Messenger.IsSupportMarkdown);
    }
}