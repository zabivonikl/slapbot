using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class UnexpectedActionHandler : AbstractHandler
{
    public UnexpectedActionHandler(IKeyboard kb, AbstractHandler? next = null) : base(kb, next)
    {
    }

    protected override bool CanHandle(Update update) => true;

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await update.Messenger.SendMessage(update.Chat, "Недопустимое действие", kb);
    }
}