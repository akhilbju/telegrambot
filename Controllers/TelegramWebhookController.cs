using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

[ApiController]
[Route("api/telegram")]
public class TelegramWebhookController : ControllerBase
{
    private readonly BotService _botService;

    public TelegramWebhookController(BotService botService)
    {
        _botService = botService;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        if (update.Message?.Text == "/start")
        {
            await _botService.sendMessage(
                update.Message.Chat.Id,
                "Welcome to Developer Interview Bot 🚀"
            );
        }

        return Ok();
    }
}
