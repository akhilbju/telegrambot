using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("📄 Resume Review", "RESUME"),
                    InlineKeyboardButton.WithCallbackData("💻 Explain Code", "CODE")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("✍ Rewrite Text", "REWRITE")
                }
            });
            await _botService.sendMessage(update.Message.Chat.Id, "Choose the Service", keyboard);
        }

        if (update.Message?.Text != "RESUME")
        {
             await _botService.sendMessage(
                update.Message.Chat.Id,
                "You are Selected Resume"
            );
        }

        if (update.Message?.Text != "CODE")
        {
             await _botService.sendMessage(
                update.Message.Chat.Id,
                "You are Selected CODE"
            );
        }

        return Ok();
    }
}
