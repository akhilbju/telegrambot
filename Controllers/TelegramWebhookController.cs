using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

[ApiController]
[Route("api/telegram")]
public class TelegramWebhookController : ControllerBase
{
    private readonly IBotService _botService;

    public TelegramWebhookController(IBotService botService)
    {
        _botService = botService;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        if (update.Message?.Text != null)
        {
            await _botService.sendMessage(
                update.Message.Chat.Id,
                "Welcome to Developer Interview Bot 🚀"
            );
        }

        return Ok();
    }
}
