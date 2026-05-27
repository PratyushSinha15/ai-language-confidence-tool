using Repository.Models;

namespace Repository.IRepository;

public interface IAnalysisRepository
{
    Task<AnalysisResult> AddAsync(AnalysisResult analysis);
    Task<List<AnalysisResult>> GetByUserIdAsync(string userId);
    Task<AnalysisResult?> GetByIdAsync(int id);
}