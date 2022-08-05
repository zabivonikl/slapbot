using Database;
using Database.Entities;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;
using User = Database.Entities.User;

namespace BotLogic.ChainResponsibilityLinks;

public class CreateGameHandler : AbstractHandler
{
    private Update update = null!;

    public CreateGameHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update incomingUpdate) => incomingUpdate.Message == "Вступить в игру";

    protected override async Task _Handle(Update incomingUpdate)
    {
        update = incomingUpdate;
        await base._Handle(update);
        await using var context = new SlapBotDal();
        try
        {
            if (!UserExist(context))
                await CreateUser(context, update.User.GetAdapter());
            else if (UserInCurrentGame(context))
                throw new InvalidOperationException("User already in this game");
            await TryCreateGame(context);
            await FindGameAndAddUser(context);
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Игра создана! Введите наказание:",
                    keyboardFactory.GetEmpty()
                );
        }
        catch (InvalidOperationException ex) when (ex.Message == "Game already exist")
        {
            await JoinToGame(context);
        }
        catch (InvalidOperationException ex1) when (ex1.Message == "User already in this game")
        {
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Вы уже участвуете.",
                    keyboardFactory.GetStartKeyboard()
                );
        }
    }

    private bool UserExist(SlapBotDal context) => context.Users.Any(u => u.Id == update.User.Id);

    private bool UserInCurrentGame(SlapBotDal context) =>
        context.Games
            .Include(g => g.Users)
            .Any(g => g.Id == update.Chat.Id && g.Users.Any(u => u.Id == update.User.Id));

    private async Task TryCreateGame(SlapBotDal context)
    {
        if (context.Games.Any(g => g.Id == update.Chat.Id))
            throw new InvalidOperationException("Game already exist");
        await CreateGame(context);
    }

    private async Task CreateGame(SlapBotDal context)
    {
        await context.Games.AddAsync(new Game(update.Chat.Id));
        await context.SaveChangesAsync();
    }

    private async Task FindGameAndAddUser(SlapBotDal context)
    {
        var game = await context.Games
            .Include(g => g.Users)
            .SingleAsync(g => g.Id == update.Chat.Id);
        game.Users.Add(update.User.GetAdapter());
        await context.SaveChangesAsync();
    }

    private static async Task CreateUser(SlapBotDal context, User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    private async Task JoinToGame(SlapBotDal context)
    {
        await FindGameAndAddUser(context);
        await update.Messenger.SendMessage(
                update.Chat,
                "Вы вступили в игру",
                keyboardFactory.GetStartKeyboard()
            );
    }
}