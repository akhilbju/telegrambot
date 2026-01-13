using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public class BotService : IBotService
{
    private readonly ITelegramBotClient _botClient;

    private readonly InlineKeyboardMarkup HomeKeyboard = new InlineKeyboardMarkup(new[]
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

    public BotService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task sendMessage(long chatId, string message)
    {
        await _botClient.SendMessage(chatId, message);
    }

    public async Task AnswerCallbackQuery(string id)
    {
        await _botClient.AnswerCallbackQuery(id);
    }

    public async Task GetWelcomeKeyboard(long chatId)
    {
        await _botClient.SendMessage(chatId, "Choose the Service", replyMarkup: HomeKeyboard);
    }

    public async Task HandleCallBackQuery(long chatId, CallbackQuery query)
    {
        var selectedOption = query.Data;
        await AnswerCallbackQuery(query.Id);

        switch (selectedOption)
        {
            case "RESUME":
                await sendMessage(chatId,"Please Send Your Resume");
                break;
        }
    }


}