using Telegram.Bot.Types;

public interface IResumeService
{
    Task ProcessResumeAsync(long chatId, Document document);
}