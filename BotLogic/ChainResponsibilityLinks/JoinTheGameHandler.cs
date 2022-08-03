﻿using System.Data;
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
        await using var context = new SlapBotDal();
        try
        {
            await TryCreateGame(context);
            await FindGameAndAddUser(context);
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Игра создана! Введите наказание:",
                    keyboardFactory.GetEmpty()
                );
        }
        catch (InvalidOperationException ex) when(ex.Message == "Game already exist")
        {
            await FindGameAndAddUser(context);
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Вы вступили в игру",
                    keyboardFactory.GetStartKeyboard()
                );
        }
        catch (InvalidOperationException ex) when(ex.Message == "User already exist")
        {
            await update.Messenger.SendMessage(
                    update.Chat,
                    "Вы уже учавствуете. Покиньте предыдущие игры.",
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
        await context.Games.AddAsync(new Game(handleableUpdate.Chat.Id, handleableUpdate.Chat.Title));
        await context.SaveChangesAsync();
    }

    private async Task FindGameAndAddUser(SlapBotDal context)
    {
        var game = await context.Games.SingleAsync(g => g.Id == handleableUpdate.Chat.Id);
        if (game.Users.All(u => u.Id != handleableUpdate.User.Id))
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