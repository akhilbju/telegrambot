public interface IBotService
{
    Task sendMessage(long chatId, string message);
}