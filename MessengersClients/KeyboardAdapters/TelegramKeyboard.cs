using Telegram.Bot.Types.ReplyMarkups;

namespace MessengersClients.KeyboardAdapters;

public class TelegramKeyboard : IKeyboard, ICloneable
{
    private readonly List<List<IKeyboardButton>> keyboard = new();

    public TelegramKeyboard(bool isInline = false)
    {
        IsInline = isInline;
        keyboard.Add(new List<IKeyboardButton>());
    }

    public object Clone() => new TelegramKeyboard(IsInline);

    public bool IsInline { get; }

    public IKeyboard AddButton(string text, ButtonColor _ = ButtonColor.Primary)
    {
        keyboard.Last().Add(IsInline ? new InlineKeyboardButton(text) : new KeyboardButton(text));
        return this;
    }

    public IKeyboard AddLine()
    {
        keyboard.Add(new List<IKeyboardButton>());
        return this;
    }

    public IReplyMarkup GetKeyboard()
    {
        if (keyboard[0].Count == 0)
            return new ReplyKeyboardRemove();
        if (IsInline)
            return new InlineKeyboardMarkup(
                    keyboard.Select(r => r.Select(b => (InlineKeyboardButton)b))
                );
        return new ReplyKeyboardMarkup(
                keyboard.Select(r => r.Select(b => (KeyboardButton)b))
            );
    }
}