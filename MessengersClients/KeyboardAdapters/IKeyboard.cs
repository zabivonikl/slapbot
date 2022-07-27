namespace MessengersClients.KeyboardAdapters;

public interface IKeyboard
{
    bool IsInline { get; }
    
    void AddButton(string text, ButtonColor color = ButtonColor.Primary);

    void AddLine();
}