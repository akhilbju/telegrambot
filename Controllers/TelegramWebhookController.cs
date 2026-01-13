using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

[ApiController]
[Route("api/telegram")]
public class TelegramWebhookController : ControllerBase
{
    private readonly IBotService _botService;
    private readonly IResumeService _resumeService;


    public TelegramWebhookController(IBotService botService,IResumeService resumeService)
    {
        _botService = botService;
        _resumeService = resumeService;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        var query = update.CallbackQuery;
        var chatId = query.Message.Chat.Id;
        if (update.CallbackQuery != null)
        {
            var selectedOption = query.Data;

            await _botService.AnswerCallbackQuery(query.Id);

            switch (selectedOption)
            {
                case "RESUME":
                    await _botService.sendMessage(
                        chatId,
                        "Please send your resume PDF"
                    );
                    break;
            }
            return Ok();
        }

        if (update.Message?.Text != null)
        {
            await _botService.sendMessage(
                chatId,
                "Welcome to Developer Interview Bot 🚀"
            );
            await _botService.GetWelcomeKeyboard(chatId);
            return Ok();
        }
        if (update.Message?.Document != null)
        {
            var document = update.Message.Document;
            await _botService.sendMessage(chatId, "Resume received. Analyzing...");
            await _resumeService.ProcessResumeAsync(chatId, document);
            return Ok();
        }

        return Ok();
    }
}
