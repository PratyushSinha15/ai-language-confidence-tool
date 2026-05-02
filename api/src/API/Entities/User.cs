namespace API.Entity;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string FirstName { get; set; }
    public string LastName { get; set; }= string.Empty;
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public List<AnalysisResult> AnalysisResults { get; set; } = new();
}