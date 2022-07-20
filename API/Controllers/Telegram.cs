using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace API.Controllers;

[ApiController]
[Route("telegram")]
public class TelegramController : ControllerBase
{
    private readonly TelegramBotClient bot;

    public TelegramController(TelegramBotClient bot)
    {
        this.bot = bot;
    }

    [HttpGet("getMe")]
    public async Task<string> GetMe() => (await bot.GetMeAsync()).ToString();
}