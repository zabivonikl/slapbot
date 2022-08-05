namespace MessengersClients.KeyboardAdapters;

public abstract class Keyboard : ICloneable
{
    protected Keyboard(bool isInline) => IsInline = isInline;

    protected bool IsInline { get; }

    public abstract Keyboard AddButton(string text, ButtonColor color = ButtonColor.Primary);

    public abstract Keyboard AddLine();

    public abstract object Clone();
}