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
        var addSlapHandler = new AddSlapHandler(keyboardFactory, deleteGameHandler);
        var startGameHandler = new StartGameHandler(keyboardFactory, addSlapHandler);
        var leavePreviousGameHandler = new LeavePreviousGameHandler(keyboardFactory, startGameHandler);
        var createGameHandler = new CreateGameHandler(keyboardFactory, leavePreviousGameHandler);
        var startMessageHandler = new StartMessageHandler(keyboardFactory, createGameHandler);
        return new SetPunishmentHandler(keyboardFactory, startMessageHandler);
    }
}