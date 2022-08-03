using MessengersClients.KeyboardFactories;
using MessengersClients.Types;

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
        await update.Messenger.SendMessage(update.Chat, "Недопустимое действие", keyboardFactory.GetStartKeyboard());
    }
}