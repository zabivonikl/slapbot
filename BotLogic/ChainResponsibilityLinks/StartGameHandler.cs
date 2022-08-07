﻿using Database;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace BotLogic.ChainResponsibilityLinks;

public class StartGameHandler : AbstractHandler
{
    public StartGameHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null) : base(keyboardFactory, next)
    {
    }

    protected override bool CanHandle(Update update) => update.Message is "Начать игру" or "Продолжить игру";

    protected override async Task _Handle(Update update)
    {
        base._Handle(update);
        await using var context = new SlapBotDal();
        try
        {
            await FindGame(context, update);
        }
        catch (InvalidOperationException)
        {
            update.Messenger.SendMessage(
                    update.Chat,
                    "Игра не найдена. Нажмите \"Вступить в игру\"",
                    keyboardFactory.GetStartKeyboard()
                );
        }
    }

    private async Task FindGame(SlapBotDal context, Update update)
    {
        var game = await context.Games.Include(g => g.Users).FirstAsync(g => g.Id == update.Chat.Id);
        update.Messenger.SendMessage(
                update.Chat,
                "Игра!",
                keyboardFactory.GetSlapKeyboard(game.Usernames)
            );
    }
}