using User = MessengersClients.Types.Chat;

namespace Database.Entities;

public class Invitation
{
    private long id;

    public Invitation(User user, Game game)
    {
        Game = game;
        User = user;
    }

    public User User { get; }

    public Game Game { get; }
}