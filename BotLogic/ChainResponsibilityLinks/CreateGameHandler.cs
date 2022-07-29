using Database;
using Database.Entities;
using MessengersClients.KeyboardAdapters;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public class CreateGameHandler : AbstractHandler
{
    public CreateGameHandler(IKeyboard kb, AbstractHandler? next = null) : base(kb, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message == "Создать игру";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await using (var context = new SlapBotDal())
        {
            var newGame = new Game();
            newGame.Users.Add(update.Chat);
            await context.Games.AddAsync(newGame);
            await context.SaveChangesAsync();
        }

        await update.Messenger.SendMessage(update.Chat, "Игра создана! Введите наказание:", kb);
    }
}