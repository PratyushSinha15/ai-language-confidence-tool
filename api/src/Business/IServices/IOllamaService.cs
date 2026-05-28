using Repository.Models;

namespace Business.IServices;

public interface IOllamaService
{
    Task<string> DetectLanguageAsync(string text);
}