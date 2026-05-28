namespace Repository.DTOs.Language;

public class DetectResponseDto
{
    public int Id { get; set; }
    public string InputText { get; set; } = null!;
    public string? Language { get; set; }
    public int? Score { get; set; }
    public double? Confidence { get; set; }
    public object? LanguageBreakdown { get; set; }
    public object? Segments { get; set; }
    public string? Explanation { get; set; }
    public string? Suggestions { get; set; }
    public string? ImprovedText { get; set; }
    public DateTime? CreatedAt { get; set; }
}