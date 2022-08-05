using MessengersClients.KeyboardAdapters;

namespace MessengersClients.KeyboardFactories;

public abstract class KeyboardFactory
{
    private readonly Keyboard keyboardProto;

    protected KeyboardFactory(Keyboard keyboardProto) => this.keyboardProto = keyboardProto;

    private Keyboard GetProtoCopy() => (Keyboard)keyboardProto.Clone();

    public Keyboard GetEmpty() => GetProtoCopy();

    public Keyboard GetStartKeyboard() =>
        GetProtoCopy().AddButton("Вступить в игру", ButtonColor.Positive).AddLine().AddButton("Начать игру");

    public Keyboard GetSlapKeyboard(IEnumerable<string> usernames)
    {
        var kb = GetProtoCopy();
        foreach (string username in usernames)
            kb.AddButton($"👋{username}").AddLine();
        kb.AddButton("Изменить наказание", ButtonColor.Secondary)
            .AddLine()
            .AddButton("Текущий счёт", ButtonColor.Positive);
        return kb;
    }

    public Keyboard GetScoreKeyboard() => GetProtoCopy()
        .AddButton("Продолжить игру", ButtonColor.Positive)
        .AddLine()
        .AddButton("Закончить игру", ButtonColor.Negative);
}