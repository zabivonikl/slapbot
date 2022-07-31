﻿using MessengersClients.KeyboardAdapters;
using MessengersClients.KeyboardFactories;
using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public abstract class AbstractHandler
{
    protected readonly KeyboardFactory keyboardFactory;

    private readonly AbstractHandler? next;

    protected AbstractHandler(KeyboardFactory keyboardFactory, AbstractHandler? next = null)
    {
        this.keyboardFactory = keyboardFactory;
        this.next = next;
    }

    public async Task Handle(Update update)
    {
        if (CanHandle(update))
            await _Handle(update);
        else
            await next!.Handle(update);
    }

    protected abstract bool CanHandle(Update update);

    protected virtual async Task _Handle(Update update) =>
        await update.Messenger.SetTyping(update.Chat);
}