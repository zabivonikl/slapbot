using Database;
using Database.Entities;
using MessengersClients.KeyboardAdapters;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class CreateGameHandler : AbstractHandler
{
    public CreateGameHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message == "Вступить в игру";

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        try
        {
            await using (var context = new SlapBotDal())
            {
                if (!context.Games.Any(g => g.Id == update.Chat.Id))
                    await CreateGame(context, update);

                await FindGameAndAddUser(context, update);
            }
            await update.Messenger.SendMessage(update.Chat, "Игра создана! Введите наказание:", keyboardFactory.GetEmpty());
        }
        catch (InvalidOperationException)
        {
            await update.Messenger.SendMessage(update.Chat, "Вы уже учавствуете", keyboardFactory.GetStartKeyboard());
        }
    }

    private static async Task CreateGame(SlapBotDal context, Update update)
    {
        await context.Games.AddAsync(new Game(update.Chat.Id, update.Chat.Title));
        await context.SaveChangesAsync();
    }

    private static async Task FindGameAndAddUser(SlapBotDal context, Update update)
    {
        var game = await context.Games.SingleAsync(g => g.Id == update.Chat.Id);
        if (!context.Users.Any(u => u.Id == update.User.Id))
            await CreateUser(context, update.User);
        else 
            throw new InvalidOperationException("User already exist");
        game.Users.Add(update.User);
        await context.SaveChangesAsync();
    }

    private static async Task CreateUser(SlapBotDal context, User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}