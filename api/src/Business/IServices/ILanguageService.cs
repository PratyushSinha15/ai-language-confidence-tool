using Repository.DTOs.Language;

namespace Business.IServices;

public interface ILanguageService
{
    Task<DetectResponseDto> DetectLanguageAsync(string userId, DetectRequestDto request);
    Task<List<DetectResponseDto>> GetHistoryAsync(string userId);
}