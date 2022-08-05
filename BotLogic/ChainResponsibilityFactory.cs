using BotLogic.ChainResponsibilityLinks;
using MessengersClients.KeyboardFactories;

namespace BotLogic;

public static class ChainResponsibilityFactory
{
    public static AbstractHandler GetChain(KeyboardFactory keyboardFactory)
    {
        var unexpectedActionHandler = new UnexpectedActionHandler(keyboardFactory);
        var editPunishmentHandler = new EditPunishmentHandler(keyboardFactory, unexpectedActionHandler);
        var deleteGameHandler = new DeleteGameHandler(keyboardFactory, editPunishmentHandler);
        var currentScoreHandler = new CurrentScoreHandler(keyboardFactory, deleteGameHandler);
        var addSlapHandler = new AddSlapHandler(keyboardFactory, currentScoreHandler);
        var startGameHandler = new StartGameHandler(keyboardFactory, addSlapHandler);
        var createGameHandler = new CreateGameHandler(keyboardFactory, startGameHandler);
        var startMessageHandler = new StartMessageHandler(keyboardFactory, createGameHandler);
        return new SetPunishmentHandler(keyboardFactory, startMessageHandler);
    }
}