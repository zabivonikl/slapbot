using Database;
using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class StartMessageHandler : AbstractHandler
{
    private readonly IKeyboard kb;

    public StartMessageHandler(IKeyboard kb, AbstractHandler? next = null) : base(next) => this.kb = kb;

    protected override bool CanHandle(Update update) => update.Message is "/start" or "Начать";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await using (var context = new SlapBotDal())
        {
            if (!context.Users.Contains(update.Chat))
                await context.Users.AddAsync(update.Chat);
            await context.SaveChangesAsync();
        }

        if (update.Messenger.IsSupportMarkdown)
            await update.Messenger.SendMarkdownMessage(update.Chat, "*Выберите действие*", kb);
        else
            await update.Messenger.SendMessage(update.Chat, "Выберите действие", kb);
    }
}