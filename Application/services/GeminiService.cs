using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _http;
    private readonly GeminiModel _options;

    public GeminiService(
        HttpClient http,
        IOptions<GeminiModel> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var url =
            $"https://generativelanguage.googleapis.com/v1beta/models/{_options.Model}:generateContent";

        var body = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(body);
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        request.Headers.Add("X-goog-api-key", _options.ApiKey);
        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseJson);

        return doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();
    }

    public async Task<string> AnalyzeResumeAsync(string resumeText)
    {
        var prompt = $"""
                You are a senior technical recruiter and resume reviewer.

                Analyze the following resume and provide:
                1. Overall resume score out of 10
                2. Strengths
                3. Weaknesses
                4. Missing skills or sections
                5. ATS optimization suggestions
                6. Grammar or formatting issues
                7. Specific improvements with examples

                Resume content:
                ----------------
                {resumeText}
                """;
        return await GenerateAsync(prompt);
    }

}
