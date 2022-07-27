using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class Telegram : Controller
{
    private readonly ITelegramBotClient botClient;

    private readonly ILogger<Telegram> logger;

    public Telegram(ITelegramBotClient bot, ILogger<Telegram> logger)
    {
        botClient = bot;
        this.logger = logger;
    }

    [HttpGet("~/[controller]")]
    public async Task<string> GetMe() => (await botClient.GetMeAsync()).ToString();

    [HttpPost]
    public IActionResult Updates([FromBody] Update update)
    {
        logger.LogInformation("{UpdateId}: {S}", update.Id, update.Message!.Chat.Username);
        Task.Factory.StartNew(
                async () => await botClient.SendTextMessageAsync(
                        update.Message!.Chat,
                        "Я робот-долбоёб"
                    )
            );
        return Ok("ok");
    }

    [HttpGet]
    public async Task<IActionResult> SetWebhook()
    {
        await botClient.SetWebhookAsync(
                "https://02f5-178-69-229-14.ngrok.io/Telegram/Updates",
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