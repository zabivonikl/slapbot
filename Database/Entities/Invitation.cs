using System.ComponentModel.DataAnnotations;
using MessengersClients.Types;

namespace Database.Entities;

public class Invitation
{
    private long id;

    [Required] public Chat Chat { get; init; } = null!;

    [Required] public Game Game { get; init; } = null!;
}