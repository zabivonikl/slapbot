using Database;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class UnexpectedActionHandler : AbstractHandler
{
    public UnexpectedActionHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(
            keyboardFactory,
            next
        )
    {
    }

    protected override bool CanHandle(Update update) => true;

    protected override async Task _Handle(Update update)
    {
        base._Handle(update);
        await using var context = new SlapBotDal();
        var game = await context.Games.Include(g => g.Users).FirstOrDefaultAsync(g => g.Id == update.Chat.Id);
        update.Messenger.SendMessage(
                update.Chat,
                "Недопустимое действие",
                game == null ? keyboardFactory.GetStartKeyboard() : keyboardFactory.GetSlapKeyboard(game.Usernames)
            );
    }
}