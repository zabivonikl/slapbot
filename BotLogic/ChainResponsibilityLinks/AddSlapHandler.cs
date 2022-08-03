using Database;
using Database.Entities;
using MessengersClients;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class AddSlapHandler : AbstractHandler
{
    public AddSlapHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message!.StartsWith("👋");

    protected override async Task _Handle(Update update)
    {
        await base._Handle(update);
        await using var context = new SlapBotDal();
        try
        {
            await AddSlap(context, update);
        }
        catch (InvalidOperationException)
        {
            await update.Messenger.SendMessage(
                    update.Chat,
                    $"Пользователь {update.Message![2..]} не найден",
                    keyboardFactory.GetSlapKeyboard(
                            context.Games
                                .Include(g => g.Users)
                                .First(g => g.Id == update.Chat.Id)
                                .Usernames
                        )
                );
        }
    }

    private async Task AddSlap(SlapBotDal context, Update update)
    {
        var game = await context.Games
            .Include(g => g.Users)
            .Include(g => g.Slaps)
            .FirstAsync(g => g.Id == update.Chat.Id);

        var from = await context.Users.FirstAsync(u => u.Id == update.User.Id);
        var to = GetUser(game, update.Message![2..]);
        var slap = CreateSlap(context, game, from, to);
        game.Slaps.Add(slap);
        await context.SaveChangesAsync();

        await update.Messenger.SendMessage(
                update.Chat,
                update.Messenger.IsSupportMarkdown ? $"_{game.Punishment!.EscapeSymbols()}_ засчитано"
                    : $"{game.Punishment!} засчитано",
                keyboardFactory.GetSlapKeyboard(game.Usernames),
                update.Messenger.IsSupportMarkdown
            );
    }

    private static User GetUser(Game game, string firstName) => game.Users.First(u => u.FirstName == firstName);

    private static Slap CreateSlap(SlapBotDal context, Game game, User from, User to)
    {
        var slap = new Slap { From = from, Game = game, To = to };
        context.Slaps.Add(slap);
        return slap;
    }
}