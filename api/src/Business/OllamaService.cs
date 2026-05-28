using Business.IServices;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Business;

public class OllamaService : IOllamaService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    private const string OllamaEndpoint =
        "http://localhost:11434/api/generate";

    private const string ModelName =
        "qwen2.5:7b";

    const string systemPrompt = @"You are a highly accurate multilingual language detection engine.

        Your responsibilities:

        - Detect all languages present in the input text.
        - Determine whether the text contains a single language or multiple languages.
        - Split the text into contiguous segments where each segment belongs to exactly one language.
        - Assign one language per segment (no overlap).
        - Provide a confidence score (0 to 1) for each segment and each language.
        - Calculate percentage contribution of each language in the entire text.
        - Ensure percentage values across all languages add up to exactly 100.

        Rules:

        1. 1. Do not modify, translate, normalize, or transliterate the original input text during analysis.
        2. improvedText may contain an optional corrected or refined version while preserving original meaning.
        3. Output MUST be valid JSON only.
        4. Preserve original text exactly in segments.
        5. Segments should be meaningful.
        6. Confidence should reflect certainty:
           - High confidence (>0.9)
           - Medium confidence (0.6–0.9)
           - Low confidence (<0.6)
        7. If only one language is present:
           - Return a single language.
        8. Always include:
           - language name
           - ISO 639-1 code
        9. Provide a short explanation describing why languages were detected.

        OUTPUT FORMAT (STRICT)

        {
          ""language"": ""<detected language name or comma separated languages>"",

          ""languageCode"": ""<ISO 639-1 code or comma separated codes>"",

          ""confidence"": <0.0-1.0>,

          ""score"": <0-100>,

          ""languageBreakdown"": {
             ""<language>"": <percentage>
          },

          ""explanation"": ""<brief explanation>"",

          ""segments"": [
            {
              ""segment"": ""<original text segment>"",
              ""language"": ""<language>"",
              ""iso639_1"": ""<ISO code>"",
              ""confidence"": <0.0-1.0>
            }
          ],

          ""suggestions"": ""<optional suggestions>"",

          ""improvedText"": ""<optional improved version>""
        }

        Strictly follow the output schema provided.
        ";

    public OllamaService(
        HttpClient httpClient,
        IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> DetectLanguageAsync(string text)
    {
        var fullPrompt =
            $"{systemPrompt}\n\nInput Text:\n{text}";

        var requestBody = new
        {
            model = ModelName,
            prompt = fullPrompt,
            stream = false,
            temperature = 0,
            format ="json"
        };

        try
        {
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(
                OllamaEndpoint,
                jsonContent);

            response.EnsureSuccessStatusCode();

            var responseText =
                await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);

            var jsonResponse =
                JsonSerializer.Deserialize<JsonElement>(
                    responseText);

            if (jsonResponse.TryGetProperty(
                    "response",
                    out var responseProperty))
            {
                var aiText = responseProperty.GetString();

                if (string.IsNullOrWhiteSpace(aiText))
                {
                    throw new Exception(
                        "Model returned empty response.");
                }

                return aiText.Trim();
            }

            throw new Exception(
                "No response field returned from Ollama.");

            throw new Exception(
                "Ollama response does not contain 'response' property.");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception(
                $"Unable to connect to Ollama. " +
                $"Make sure Ollama is running. Details: {ex.Message}",
                ex);
        }
        catch (JsonException ex)
        {
            throw new Exception(
                $"Invalid JSON returned from Ollama. Details: {ex.Message}",
                ex);
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Ollama service error: {ex.Message}",
                ex);
        }
    }
}