namespace MessengersClients.KeyboardAdapters;

public interface IKeyboard
{
    bool IsInline { get; }

    IKeyboard AddButton(string text, ButtonColor color = ButtonColor.Primary);

    IKeyboard AddLine();
}