using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class UnexpectedActionHandler : AbstractHandler
{
    protected override bool CanHandle(Update update) => true;

    protected override Task _Handle(Update update)
    {
        base._Handle(update);
        return update.Messenger.SupportMarkdown ?
            update.Messenger.SendMessage(update.Chat, "Недопустимое действие") :
            update.Messenger.SendMarkdownMessage(update.Chat, "_Недопустимое действие_");
    }
}