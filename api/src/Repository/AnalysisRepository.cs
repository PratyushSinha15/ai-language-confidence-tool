using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using Repository.Models;

namespace Repository;

public class AnalysisRepository:IAnalysisRepository
{
    private readonly AppDbContext _context;

    public AnalysisRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async  Task<AnalysisResult> AddAsync(AnalysisResult analysis)
    {
        analysis.CreatedAt = DateTime.Now;
        await _context.AnalysisResults.AddAsync(analysis);
        await _context.SaveChangesAsync();
        return analysis;
    }

    public async Task<List<AnalysisResult>> GetByUserIdAsync(string userId)
    {
        return await _context.AnalysisResults.Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();

    }

    public async Task<AnalysisResult?> GetByIdAsync(int id)
    {
        return await _context.AnalysisResults.FirstOrDefaultAsync(a => a.Id == id);
    }
}