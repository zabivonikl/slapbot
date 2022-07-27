using BotLogic;
using BotLogic.ChainResponsibilityLinks;
using MessengersClients;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class Telegram : Controller
{
    private readonly AbstractHandler eventHandler;
    
    private readonly ITelegramBotClient botClient;

    private readonly ILogger<Telegram> logger;

    public Telegram(ITelegramBotClient bot, ILogger<Telegram> logger)
    {
        eventHandler = ChainResponsibilityFactory.GetChain();
        botClient = bot;
        this.logger = logger;
    }

    [HttpGet("~/[controller]")]
    public async Task<string> GetMe() => (await botClient.GetMeAsync()).ToString();

    [HttpPost]
    public async Task<IActionResult> Updates([FromBody] Update update)
    {
        logger.LogInformation("{UpdateId}: {S}", update.Id, update.Message!.Chat.Username);
        if (update.Type == UpdateType.Message)
            await eventHandler.Handle(update.GetAdapter(botClient));
        return Ok("ok");
    }

    [HttpGet]
    public async Task<IActionResult> SetWebhook()
    {
        await botClient.SetWebhookAsync(
                "https://d994-178-69-229-14.ngrok.io/Telegram/Updates",
                dropPendingUpdates: true
            );
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> RemoveWebhook()
    {
        await botClient.DeleteWebhookAsync();
        return Ok();
    }
}