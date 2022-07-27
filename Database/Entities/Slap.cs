using System.ComponentModel.DataAnnotations;
using MessengersClients.Types;

namespace Database.Entities;

public class Slap
{
    private long id;

    public Slap() => Time = DateTime.Now;

    [Required]
    public Game Game { get; init; } = null!;

    [Required]
    public Chat From { get; init; } = null!;

    [Required]
    public Chat To { get; init; } = null!;

    public DateTime Time { get; }
}