using BotLogic.ChainResponsibilityLinks;
using MessengersClients.KeyboardFactories;

namespace BotLogic;

public static class ChainResponsibilityFactory
{
    public static AbstractHandler GetChain(KeyboardFactory keyboardFactory)
    {
        var unexpectedActionHandler = new UnexpectedActionHandler(keyboardFactory.GetEmpty());
        var deleteGameHandler = new DeleteGameHandler(keyboardFactory.GetStartKeyboard(), unexpectedActionHandler);
        var createGameHandler = new CreateGameHandler(keyboardFactory.GetEmpty(), deleteGameHandler);
        var startMessageHandler = new StartMessageHandler(keyboardFactory.GetStartKeyboard(), createGameHandler);
        return new SetPunishmentHandler(keyboardFactory.GetEmpty(), startMessageHandler);
    }
}