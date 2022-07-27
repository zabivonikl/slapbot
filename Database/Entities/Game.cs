using User = MessengersClients.Types.Chat;

namespace Database.Entities;

public class Game
{
    private long id;

    public string? Punishment { get; set; }

    public IEnumerable<User> Users { get; } = new List<User>();

    public IEnumerable<Slap> Slaps { get; } = new List<Slap>();
}