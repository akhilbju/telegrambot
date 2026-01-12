using Telegram.Bot;

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

}