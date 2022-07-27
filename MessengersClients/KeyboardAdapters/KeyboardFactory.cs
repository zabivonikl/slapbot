namespace MessengersClients.KeyboardAdapters;

public abstract class KeyboardFactory
{
    public abstract IKeyboard GetEmpty();
    
    public abstract IKeyboard GetStartKeyboard();
    
    protected static IKeyboard GetEmpty(IKeyboard kb) => kb;

    protected static IKeyboard GetStartKeyboard(IKeyboard kb)
    {
        kb.AddButton("Создать игру", ButtonColor.Positive);
        kb.AddLine();
        kb.AddButton("Ждать приглашение", ButtonColor.Secondary);
        return kb;
    }
}