using System.ComponentModel.DataAnnotations;

namespace Database.Entities;

public class Slap
{
    private long id;

    private Slap()
    {
    }

    public Slap(Game game, User from, User to)
    {
        Game = game;
        From = from;
        To = to;
    }

    [Required] public Game Game { get; }

    [Required] public User From { get; }

    [Required] public User To { get; }
}