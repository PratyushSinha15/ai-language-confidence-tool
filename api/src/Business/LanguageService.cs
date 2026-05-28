using System.Text.Json;
using Business.IServices;
using Repository.DTOs.Language;
using Repository.IRepository;
using Repository.Models;

namespace Business;

public class LanguageService :ILanguageService
{
    private  IOllamaService _ollamaService;
    private readonly IAnalysisRepository _analysisRepository;

    public LanguageService(IOllamaService ollamaService, IAnalysisRepository analysisRepository)
    {
        _ollamaService = ollamaService;
        _analysisRepository = analysisRepository;
    }

    public async Task<DetectResponseDto> DetectLanguageAsync(string userId, DetectRequestDto request)
    {
        //ollama lang detection calling
        var aiResponse = await _ollamaService.DetectLanguageAsync(request.InputText);
        if (string.IsNullOrWhiteSpace(aiResponse))
        {
            throw new Exception(
                "Empty AI response.");
        }
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(aiResponse);
        
        var analysis = new AnalysisResult
        {
            UserId = userId,
            InputText = request.InputText,
            Language = jsonResponse.TryGetProperty("language", out var lang) ? lang.GetString() : null,
            Confidence = jsonResponse.TryGetProperty("confidence", out var conf) ? conf.GetDouble() : null,
            Score = jsonResponse.TryGetProperty("score", out var score) ? score.GetInt32() : null,
            Explanation = jsonResponse.TryGetProperty("explanation", out var exp) ? exp.GetString() : null,
            Suggestions = jsonResponse.TryGetProperty("suggestions", out var sug) ? sug.GetString() : null,
            ImprovedText = jsonResponse.TryGetProperty("improvedText", out var improved) ? improved.GetString() : null,
            LanguageBreakdown = jsonResponse.TryGetProperty("languageBreakdown", out var breakdown) ? breakdown.GetRawText() : null,
            Segments = jsonResponse.TryGetProperty("segments", out var segments) ? segments.GetRawText() : null
        };
        
        var savedAnalysis= await _analysisRepository.AddAsync(analysis);
        
        return new DetectResponseDto
        {
            Id = savedAnalysis.Id,
            InputText = savedAnalysis.InputText,
            Language = savedAnalysis.Language,
            Confidence = savedAnalysis.Confidence,
            Score = savedAnalysis.Score,
            Explanation = savedAnalysis.Explanation,
            Suggestions = savedAnalysis.Suggestions,
            ImprovedText = savedAnalysis.ImprovedText,
            LanguageBreakdown = string.IsNullOrWhiteSpace(savedAnalysis.LanguageBreakdown) ? null : JsonSerializer.Deserialize<object>(savedAnalysis.LanguageBreakdown),
            Segments = string.IsNullOrWhiteSpace(savedAnalysis.Segments) ? null : JsonSerializer.Deserialize<object>(savedAnalysis.Segments),
            CreatedAt = savedAnalysis.CreatedAt
        };
    }

    public async Task<List<DetectResponseDto>> GetHistoryAsync(string userId)
    {
        var results = await _analysisRepository.GetByUserIdAsync(userId);

        return results.Select(r => new DetectResponseDto
        {
            Id = r.Id,
            InputText = r.InputText,
            Language = r.Language,
            Confidence = r.Confidence,
            Score = r.Score,
            Explanation = r.Explanation,
            Suggestions = r.Suggestions,
            ImprovedText = r.ImprovedText,
            LanguageBreakdown = string.IsNullOrWhiteSpace(r.LanguageBreakdown) ? null : JsonSerializer.Deserialize<object>(r.LanguageBreakdown),
            Segments = string.IsNullOrWhiteSpace(r.Segments) ? null : JsonSerializer.Deserialize<object>(r.Segments),
            CreatedAt = r.CreatedAt
        }).ToList();
    }

    
}