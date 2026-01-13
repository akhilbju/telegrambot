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
        if (update.CallbackQuery != null)
        {
            var query = update.CallbackQuery;
            var chatId = query.Message.Chat.Id;
            var selectedOption = query.Data;

            await _botService.AnswerCallbackQuery(query.Id);

            switch (selectedOption)
            {
                case "RESUME":
                    await _botService.sendMessage(
                        chatId,
                        "Please send your resume text or PDF"
                    );
                    break;
            }

            return Ok();
        }

        if (update.Message?.Text != null)
        {
            var chatId = update.Message.Chat.Id;

            await _botService.sendMessage(
                chatId,
                "Welcome to Developer Interview Bot 🚀"
            );

            await _botService.GetWelcomeKeyboard(chatId);

            return Ok();
        }

        return Ok();
    }
}
