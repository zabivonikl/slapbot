using Telegram.Bot.Types.ReplyMarkups;

namespace MessengersClients.KeyboardAdapters;

public class TelegramKeyboard : Keyboard
{
    private readonly List<List<IKeyboardButton>> keyboard = new();

    public TelegramKeyboard(bool isInline = false) : base(isInline) => keyboard.Add(new List<IKeyboardButton>());

    public override object Clone() => new TelegramKeyboard(IsInline);

    public override Keyboard AddButton(string text, ButtonColor _ = ButtonColor.Primary)
    {
        keyboard.Last().Add(IsInline ? new InlineKeyboardButton(text) : new KeyboardButton(text));
        return this;
    }

    public override Keyboard AddLine()
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