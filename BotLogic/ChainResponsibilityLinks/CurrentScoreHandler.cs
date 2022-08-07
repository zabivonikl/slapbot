using Database;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class CurrentScoreHandler : AbstractHandler
{
    public CurrentScoreHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message == "Текущий счёт";

    protected override async Task _Handle(Update update)
    {
        base._Handle(update);
        await using var context = new SlapBotDal();
        var game = await context.Games
            .Include(g => g.Slaps)
            .Include(g => g.Users)
            .FirstAsync(g => g.Id == update.Chat.Id);
        update.Messenger.SendMessage(
                update.Chat,
                update.Messenger.IsSupportMarkdown ? game.GetMarkdownResult() : game.GetResult(),
                keyboardFactory.GetScoreKeyboard(),
                update.Messenger.IsSupportMarkdown
            );
    }
}