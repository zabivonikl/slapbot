using BotLogic.ChainResponsibilityLinks;
using MessengersClients.KeyboardAdapters;

namespace BotLogic;

public static class ChainResponsibilityFactory
{
    public static AbstractHandler GetChain(KeyboardFactory keyboardFactory)
    {
        var unexpectedActionHandler = new UnexpectedActionHandler(keyboardFactory.GetEmpty());
        return new StartMessageHandler(keyboardFactory.GetStartKeyboard(),unexpectedActionHandler);
    }
}