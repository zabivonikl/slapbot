using MessengersClients.KeyboardFactories;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class StartMessageHandler : AbstractHandler
{
    public StartMessageHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message is "/start" or "Начать";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await update.Messenger.SendMessage(update.Chat, "Выберите действие", keyboardFactory.GetStartKeyboard());
    }
}