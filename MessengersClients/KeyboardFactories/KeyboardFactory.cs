using MessengersClients.KeyboardAdapters;

namespace MessengersClients.KeyboardFactories;

public abstract class KeyboardFactory
{
    public abstract IKeyboard GetEmpty();

    public abstract IKeyboard GetStartKeyboard();

    protected static IKeyboard GetEmpty(IKeyboard kb) => kb;

    protected static IKeyboard GetStartKeyboard(IKeyboard kb) => kb.AddButton("Вступить в игру", ButtonColor.Positive);
}