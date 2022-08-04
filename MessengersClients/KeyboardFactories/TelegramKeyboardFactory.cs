using MessengersClients.KeyboardAdapters;

namespace MessengersClients.KeyboardFactories;

public class TelegramKeyboardFactory : KeyboardFactory
{
    public TelegramKeyboardFactory() => keyboardProto = new TelegramKeyboard();
}