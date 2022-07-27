using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class UnexpectedActionHandler : AbstractHandler
{
    private readonly IKeyboard kb;

    public UnexpectedActionHandler(IKeyboard kb, AbstractHandler? next = null) : base(next) => this.kb = kb;

    protected override bool CanHandle(Update update) => true;

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        if (update.Messenger.IsSupportMarkdown)
            await update.Messenger.SendMarkdownMessage(update.Chat, "_Недопустимое действие_", kb);
        else
            await update.Messenger.SendMessage(update.Chat, "Недопустимое действие", kb);
    }
}