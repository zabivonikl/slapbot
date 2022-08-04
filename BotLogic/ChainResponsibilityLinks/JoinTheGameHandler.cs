using Database;
using Database.Entities;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;
using User = Database.Entities.User;

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
        await using var context = new SlapBotDal();
        try
        {
            if (context.Users.All(u => u.Id != handleableUpdate.User.Id))
                await CreateUser(context, handleableUpdate.User.GetAdapter());
            else if (context.Games.Include(g => g.Users).Any(
                             g => g.Id == handleableUpdate.Chat.Id && g.Users.Any(u => u.Id == handleableUpdate.User.Id)
                         ))
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
            await handleableUpdate.Messenger.SendMessage(
                    handleableUpdate.Chat,
                    "Вы уже участвуете.",
                    keyboardFactory.GetStartKeyboard()
                );
        }
    }

    private async Task TryCreateGame(SlapBotDal context)
    {
        if (context.Games.Any(g => g.Id == handleableUpdate.Chat.Id))
            throw new InvalidOperationException("Game already exist");
        await CreateGame(context);
    }

    private async Task CreateGame(SlapBotDal context)
    {
        await context.Games.AddAsync(new Game(handleableUpdate.Chat.Id));
        await context.SaveChangesAsync();
    }

    private async Task FindGameAndAddUser(SlapBotDal context)
    {
        var game = await context.Games
            .Include(g => g.Users)
            .SingleAsync(g => g.Id == handleableUpdate.Chat.Id);
        game.Users.Add(handleableUpdate.User.GetAdapter());
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
        await handleableUpdate.Messenger.SendMessage(
                handleableUpdate.Chat,
                "Вы вступили в игру",
                keyboardFactory.GetStartKeyboard()
            );
    }
}