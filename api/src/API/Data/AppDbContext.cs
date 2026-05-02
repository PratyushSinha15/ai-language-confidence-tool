using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<AnalysisResult> Results { get; set; }
}