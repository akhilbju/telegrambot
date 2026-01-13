using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

public interface IBotService
{
    Task sendMessage(long chatId, string message);
    Task AnswerCallbackQuery(string id);
    Task GetWelcomeKeyboard(long id);
    Task HandleCallBackQuery(long id, CallbackQuery query);
}