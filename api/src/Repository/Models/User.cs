

namespace Repository.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AnalysisResult> AnalysisResults { get; set; } = new List<AnalysisResult>();
}
