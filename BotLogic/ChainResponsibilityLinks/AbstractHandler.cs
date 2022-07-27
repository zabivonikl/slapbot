using MessengersClients.Types;

namespace BotLogic.ChainResponsibilityLinks;

public abstract class AbstractHandler
{
    private readonly AbstractHandler? next;

    protected AbstractHandler(AbstractHandler? next = null) => this.next = next;

    public async Task Handle(Update update)
    {
        if (CanHandle(update))
            await _Handle(update);
        else
            await next!.Handle(update);
    }

    protected abstract bool CanHandle(Update update);

    protected abstract Task _Handle(Update update);
}