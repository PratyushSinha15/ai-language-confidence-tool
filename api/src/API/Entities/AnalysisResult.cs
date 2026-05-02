namespace API.Entity;

public class AnalysisResult
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    
    public User User { get; set; } = null!;
    public string InputText { get; set; } = string.Empty;
    public string OutputText { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    
    public int Score { get; set; }
    public string Suggestions { get; set; } = string.Empty;
    public string ImprovedTexts { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}