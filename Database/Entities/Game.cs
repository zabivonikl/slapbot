using System.ComponentModel.DataAnnotations;
using MessengersClients.Types;

namespace Database.Entities;

public class Game
{
    private long id;

    [Required] public string? Punishment { get; set; }

    public IEnumerable<Chat> Users { get; } = new List<Chat>();

    public IEnumerable<Slap> Slaps { get; } = new List<Slap>();
}