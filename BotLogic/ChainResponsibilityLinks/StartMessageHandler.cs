using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class StartMessageHandler : AbstractHandler
{
    public StartMessageHandler(AbstractHandler? next = null) : base(next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message is "/start" or "Начать";

    protected override Task _Handle(Update update)
    {
        base._Handle(update);
        return update.Messenger.SupportMarkdown ?
            update.Messenger.SendMessage(update.Chat, "Выберите действие") :
            update.Messenger.SendMarkdownMessage(update.Chat, "*Выберите действие*");
    }
}