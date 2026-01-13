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
        var chatId = update.Message.Chat.Id;

        if (update.Message?.Text != null)
        {
            await _botService.sendMessage(
                chatId,
                "Welcome to Developer Interview Bot 🚀"
            );
            await _botService.GetWelcomeKeyboard(chatId);
        }

        if (update.CallbackQuery != null)
        {
            await _botService.HandleCallBackQuery(chatId,update.CallbackQuery);
        }

        return Ok();
    }
}
