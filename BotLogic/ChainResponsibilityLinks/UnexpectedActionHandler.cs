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
        await base._Handle(update);
        await using var context = new SlapBotDal();
        var game = context.Games.Include(g => g.Users).FirstOrDefault(g => g.Id == update.Chat.Id);
        await update.Messenger.SendMessage(
            update.Chat, 
            "Недопустимое действие", 
            game == null ? keyboardFactory.GetStartKeyboard() : keyboardFactory.GetSlapKeyboard(game.Usernames));
    }
}