using Database;
using Database.Entities;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class CreateGameHandler : AbstractHandler
{
    private Update handleableUpdate = null!;

    public CreateGameHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message == "Вступить в игру";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        handleableUpdate = update;
        try
        {
            await TryCreateGameOrAddUser();
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Игра создана! Введите наказание:",
                    keyboardFactory.GetEmpty()
                );
        }
        catch (InvalidOperationException)
        {
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Вы уже учавствуете",
                    keyboardFactory.GetStartKeyboard()
                );
        }
    }

    private async Task TryCreateGameOrAddUser()
    {
        await using var context = new SlapBotDal();
        if (!context.Games.Any(g => g.Id == handleableUpdate.Chat.Id))
            await CreateGame(context);

        await FindGameAndAddUser(context);
    }

    private async Task CreateGame(SlapBotDal context)
    {
        await context.Games.AddAsync(new Game(handleableUpdate.Chat.Id, handleableUpdate.Chat.Title));
        await context.SaveChangesAsync();
    }

    private async Task FindGameAndAddUser(SlapBotDal context)
    {
        var game = await context.Games.SingleAsync(g => g.Id == handleableUpdate.Chat.Id);
        if (!context.Users.Any(u => u.Id == handleableUpdate.User.Id))
            await CreateUser(context, handleableUpdate.User);
        else
            throw new InvalidOperationException("User already exist");
        game.Users.Add(handleableUpdate.User);
        await context.SaveChangesAsync();
    }

    private static async Task CreateUser(SlapBotDal context, User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}