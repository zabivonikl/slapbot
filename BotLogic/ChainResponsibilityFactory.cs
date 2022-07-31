using BotLogic.ChainResponsibilityLinks;
using MessengersClients.KeyboardFactories;

namespace BotLogic;

public static class ChainResponsibilityFactory
{
    public static AbstractHandler GetChain(KeyboardFactory keyboardFactory)
    {
        var unexpectedActionHandler = new UnexpectedActionHandler(keyboardFactory);
        var deleteGameHandler = new DeleteGameHandler(keyboardFactory, unexpectedActionHandler);
        var createGameHandler = new CreateGameHandler(keyboardFactory, deleteGameHandler);
        var startMessageHandler = new StartMessageHandler(keyboardFactory, createGameHandler);
        return new SetPunishmentHandler(keyboardFactory, startMessageHandler);
    }
}