using MessengersClients.Types;

namespace Database.Entities;

public partial class Game
{
    private IDictionary<Chat, int> GetScore() =>
        Slaps.GroupBy(s => s.From).ToDictionary(g => g.Key, g => g.Count());

    public string GetMarkdownResult() =>
        GetScore().Aggregate("*Счёт*:\n", (current, userScore) => current + $"{userScore.Key}: _{userScore.Value}_\n");

    public string GetResult() =>
        GetScore().Aggregate("Счёт:\n", (current, userScore) => current + $"{userScore.Key}: {userScore.Value}\n");
}