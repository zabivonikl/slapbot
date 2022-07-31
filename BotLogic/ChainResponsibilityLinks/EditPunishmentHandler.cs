using Database;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class EditPunishmentHandler : AbstractHandler
{
    public EditPunishmentHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message == "Изменить наказание";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await using (var context = new SlapBotDal())
        {
            context.Games
                .First(g => g.Id == update.Chat.Id)
                .Punishment = null;
            await context.SaveChangesAsync();
        }

        await update.Messenger.SendMessage(update.Chat, "Введите наказание:", keyboardFactory.GetEmpty());
    }
}