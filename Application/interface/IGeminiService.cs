public interface IGeminiService
{
    Task<string> GenerateAsync(string prompt);
    Task<string> AnalyzeResumeAsync(string resumeText);
}