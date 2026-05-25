using Repository.Models;

namespace API.Entity;

public partial class AnalysisResult
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string InputText { get; set; } = null!;

    public string? OutputText { get; set; }

    public string? Language { get; set; }

    public int? Score { get; set; }

    public double? Confidence { get; set; }

    public string? LanguageBreakdown { get; set; }

    public string? Segments { get; set; }

    public string? Explanation { get; set; }

    public string? Suggestions { get; set; }

    public string? ImprovedText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
