using MessengersClients.KeyboardAdapters;

namespace MessengersClients.KeyboardFactories;

public abstract class KeyboardFactory
{
    private readonly IKeyboard keyboardProto;

    protected KeyboardFactory(IKeyboard keyboardProto) => this.keyboardProto = keyboardProto;

    private IKeyboard GetProtoCopy() => (IKeyboard)keyboardProto.Clone();

    public IKeyboard GetEmpty() => GetProtoCopy();

    public IKeyboard GetStartKeyboard() =>
        GetProtoCopy().AddButton("Вступить в игру", ButtonColor.Positive).AddLine().AddButton("Начать игру");

    public IKeyboard GetSlapKeyboard(IEnumerable<string> usernames)
    {
        var kb = GetProtoCopy();
        foreach (string username in usernames)
            kb.AddButton($"👋{username}").AddLine();
        kb.AddButton("Изменить наказание", ButtonColor.Secondary)
            .AddLine()
            .AddButton("Закончить игру", ButtonColor.Negative);
        return kb;
    }
}