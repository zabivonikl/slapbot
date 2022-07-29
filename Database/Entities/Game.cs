using MessengersClients.Types;

namespace Database.Entities;

public partial class Game
{
    private long id;

    public string? Punishment { get; set; }

    public List<Chat> Users { get; } = new();

    public List<Slap> Slaps { get; } = new();
}