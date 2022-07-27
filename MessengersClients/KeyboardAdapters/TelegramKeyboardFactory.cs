namespace MessengersClients.KeyboardAdapters;

public class TelegramKeyboardFactory : KeyboardFactory
{
    public override IKeyboard GetEmpty() => GetEmpty(new TelegramKeyboard());
    
    public override IKeyboard GetStartKeyboard() => GetStartKeyboard(new TelegramKeyboard());
}