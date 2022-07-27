using User = MessengersClients.Types.Chat;

namespace Database.Entities;

public class Slap
{
    private long id;

    public Slap(Game game, User from, User to)
    {
        Game = game;
        From = from;
        To = to;
        Time = DateTime.Now;
    }

    public Game Game { get; }

    public User From { get; }

    public User To { get; }

    public DateTime Time { get; }
}