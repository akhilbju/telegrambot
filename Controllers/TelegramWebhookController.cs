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

        if (update.CallbackQuery != null)
        {
            var selectedOption = update.CallbackQuery.Data;
            var chatId = update.CallbackQuery.Message.Chat.Id;
            await _botService.AnswerCallbackQuery(update.CallbackQuery.Id);

            switch (selectedOption)
            {
                case "RESUME":
                    await HandleResume(chatId);
                    break;

                // case "CODE":
                //     await HandleCode();
                //     break;

                // case "REWRITE":
                //     await HandleRewrite();
                //     break;
            }
        }


        return Ok();
    }

    private async Task HandleResume(long chatId)
    {
        await _botService.sendMessage(
            chatId,
            "Send your resume text or PDF."
        );

    }
}
