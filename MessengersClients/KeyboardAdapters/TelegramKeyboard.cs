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

    public IReplyMarkup GetKeyboard()
    {
        if (keyboard.Count == 1 && keyboard[0].Count == 0) 
            return new ReplyKeyboardRemove();
        if (IsInline) 
            return new InlineKeyboardMarkup(
                keyboard.Select(r => r.Select(b => (InlineKeyboardButton)b)));
        return new ReplyKeyboardMarkup(
            keyboard.Select(r => r.Select(b => (KeyboardButton)b)));
    }

    public bool IsInline { get; }

    public void AddButton(string text, ButtonColor _ = ButtonColor.Primary) => 
        keyboard.Last().Add(IsInline ? new InlineKeyboardButton(text) : new KeyboardButton(text));

    public void AddLine() => keyboard.Add(new List<IKeyboardButton>());

    public object Clone() => new TelegramKeyboard(IsInline);
}