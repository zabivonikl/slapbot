using System.ComponentModel.DataAnnotations.Schema;
using MessengersClients;
using MessengersClients.Types;

namespace Database.Entities;

public class Game
{
    public Game(long id, string? title = null)
    {
        Id = id;
        Title = title;
    }

    public long Id { get; init; }

    public string? Title { get; init; }

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