using MessengersClients.KeyboardAdapters;

namespace MessengersClients.KeyboardFactories;

public class TelegramKeyboardFactory : KeyboardFactory
{
    public TelegramKeyboardFactory() : base(new TelegramKeyboard())
    {
    }
}