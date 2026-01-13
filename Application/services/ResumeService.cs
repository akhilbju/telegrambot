using System.Text;
using Telegram.Bot.Types;
using UglyToad.PdfPig;

public class ResumeService : IResumeService
{
    private readonly IBotService _botService;
    private readonly IGeminiService _geminiService;

    public ResumeService(IBotService botService,IGeminiService geminiService)
    {
        _botService = botService;
        _geminiService = geminiService;
    }
    public async Task ProcessResumeAsync(long chatId, Document document)
    {
        var pdfBytes = await _botService.DownloadTelegramFileAsync(document.FileId);
        var resumeText = ExtractTextFromPdf(pdfBytes);
        if (resumeText.Length > 12000)
            resumeText = resumeText.Substring(0, 12000);
        var aiResponse = await _geminiService.AnalyzeResumeAsync(resumeText);
        await _botService.sendMessage(chatId, aiResponse);
    }

    private string ExtractTextFromPdf(byte[] pdfBytes)
    {
        using var ms = new MemoryStream(pdfBytes);
        using var document = PdfDocument.Open(ms);
        var sb = new StringBuilder();
        foreach (var page in document.GetPages())
        {
            sb.AppendLine(page.Text);
        }
        return sb.ToString();
    }

}