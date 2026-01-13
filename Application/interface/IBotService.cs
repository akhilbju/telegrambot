using Telegram.Bot.Types.ReplyMarkups;

public interface IBotService
{
    Task sendMessage(long chatId, string message);
    Task sendMessage(long chatId, string message, InlineKeyboardMarkup keyboard);
}