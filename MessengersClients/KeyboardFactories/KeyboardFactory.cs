using MessengersClients.KeyboardAdapters;

namespace MessengersClients.KeyboardFactories;

public abstract class KeyboardFactory
{
    public abstract IKeyboard GetEmpty();

    public abstract IKeyboard GetStartKeyboard();

    public abstract IKeyboard GetSlapKeyboard(IEnumerable<string> usernames);

    protected static IKeyboard GetEmpty(IKeyboard kb) => kb;

    protected static IKeyboard GetStartKeyboard(IKeyboard kb) =>
        kb.AddButton("Вступить в игру", ButtonColor.Positive).AddLine().AddButton("Начать игру");

    protected static IKeyboard GetSlapKeyboard(IKeyboard kb, IEnumerable<string> usernames)
    {
        foreach (string username in usernames)
            kb.AddButton($"👋{username}").AddLine();
        kb.AddButton("Изменить наказание", ButtonColor.Secondary)
            .AddLine()
            .AddButton("Закончить игру", ButtonColor.Negative);
        return kb;
    }
}