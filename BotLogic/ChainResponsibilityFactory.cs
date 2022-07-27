using BotLogic.ChainResponsibilityLinks;

namespace BotLogic;

public static class ChainResponsibilityFactory
{
    public static AbstractHandler GetChain()
    {
        var unexpectedActionHandler = new UnexpectedActionHandler();
        return new StartMessageHandler(unexpectedActionHandler);
    }
}