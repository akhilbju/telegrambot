using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

public class BotService : IBotService
{
    private readonly ITelegramBotClient _botClient;
    public BotService(ITelegramBotClient  botClient)
    {
        _botClient = botClient;
    }

    public async Task sendMessage(long chatId, string message)
    {
         await _botClient.SendMessage(chatId,message);
    }

    public async Task sendMessage(long chatId, string message, InlineKeyboardMarkup keyboard)
    {
        await _botClient.SendMessage(chatId,message, replyMarkup:keyboard);
    }

    public async Task AnswerCallbackQuery(string id)
    {
        await _botClient.AnswerCallbackQuery(id);
    }

}