using System.ComponentModel.DataAnnotations.Schema;
using MessengersClients;

namespace Database.Entities;

public class Game
{
    private Game()
    {
    }

    public Game(long id) => Id = id;

    public long Id { get; }

    public string? Punishment { get; set; }

    public List<User> Users { get; } = new();

    public List<Slap> Slaps { get; } = new();

    [NotMapped] public IEnumerable<string> Usernames => Users.AsParallel().Select(u => u.FirstName);

    private IReadOnlyDictionary<User, int> GetScore() =>
        Slaps.GroupBy(s => s.To).ToDictionary(g => g.Key, g => g.Count());

    public string GetMarkdownResult() =>
        GetScore().Aggregate(
                "*Счёт*:\n",
                (current, userScore) =>
                    current +
                    $"{userScore.Key.FirstName.EscapeSymbols()}: _{userScore.Value} {Punishment!.EscapeSymbols()}_\n"
            );

    public string GetResult() =>
        GetScore().Aggregate(
                "Счёт:\n",
                (current, userScore) => current + $"{userScore.Key}: {userScore.Value} {Punishment}\n"
            );
}