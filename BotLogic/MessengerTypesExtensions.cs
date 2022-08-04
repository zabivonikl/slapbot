using Database.Entities;

namespace BotLogic;

public static class MessengerTypesExtensions
{
    public static User GetAdapter(this MessengersClients.Types.User user) => new(user);
}