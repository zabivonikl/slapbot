using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Telegram : Controller
{
    private readonly ILogger<Telegram> logger;

    private readonly ITelegramBotClient botClient;

    public Telegram(ITelegramBotClient bot, ILogger<Telegram> logger)
    {
        botClient = bot;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<string> GetMe() => (await botClient.GetMeAsync()).ToString();

    [HttpPost]
    public IActionResult Updates(Update update)
    {
        logger.LogDebug("{UpdateId}: {S}", update.Id, update.Type.ToString());
        switch (update.Type)
        {
            case UpdateType.Message:
            {
                Task.Run(
                        async () => await botClient.SendTextMessageAsync(
                                update.Message!.Chat,
                                $"{update.Id}: {update.Type.ToString()}"
                            )
                    ).Start();
                break;
            }
            case UpdateType.Unknown:
                break;
            case UpdateType.InlineQuery:
                break;
            case UpdateType.ChosenInlineResult:
                break;
            case UpdateType.CallbackQuery:
                break;
            case UpdateType.EditedMessage:
                break;
            case UpdateType.ChannelPost:
                break;
            case UpdateType.EditedChannelPost:
                break;
            case UpdateType.ShippingQuery:
                break;
            case UpdateType.PreCheckoutQuery:
                break;
            case UpdateType.Poll:
                break;
            case UpdateType.PollAnswer:
                break;
            case UpdateType.MyChatMember:
                break;
            case UpdateType.ChatMember:
                break;
            case UpdateType.ChatJoinRequest:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(update));
        }
        return Ok("ok");
    }
}