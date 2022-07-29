using BotLogic;
using BotLogic.ChainResponsibilityLinks;
using MessengersClients;
using MessengersClients.KeyboardFactories;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class Telegram : Controller
{
    private readonly string baseUrl;

    private readonly ITelegramBotClient botClient;

    private readonly AbstractHandler eventHandler;

    private readonly ILogger<Telegram> logger;

    public Telegram(IConfiguration configuration, ITelegramBotClient bot, ILogger<Telegram> logger)
    {
        eventHandler = ChainResponsibilityFactory.GetChain(new TelegramKeyboardFactory());
        botClient = bot;
        this.logger = logger;
        baseUrl = configuration["Url"];
    }

    [HttpGet("~/[controller]")]
    public async Task<string> GetMe() => (await botClient.GetMeAsync()).ToString();

    [HttpPost]
    public async Task<IActionResult> Updates([FromBody] Update update)
    {
        logger.LogInformation("{UpdateId}: {S}", update.Id, update.Type.ToString());
        if (update.Type == UpdateType.Message)
            await eventHandler.Handle(update.GetAdapter(botClient));
        return Ok("ok");
    }

    [HttpGet]
    public async Task<IActionResult> SetWebhook()
    {
        await botClient.SetWebhookAsync(
                $"{baseUrl}/Telegram/Updates",
                dropPendingUpdates: true
            );
        logger.LogInformation("Set webhook for URL: {S}", $"{baseUrl}/Telegram/Updates");
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> RemoveWebhook()
    {
        await botClient.DeleteWebhookAsync();
        logger.LogInformation("Webhook removed");
        return Ok();
    }

    ~Telegram() => Task.Factory.StartNew(RemoveWebhook);
}