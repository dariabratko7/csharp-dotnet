using Microsoft.EntityFrameworkCore;

namespace WebPageDownloader;

public class AppDbContext : DbContext
{
    public DbSet<PageResult> PageResults => Set<PageResult>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=pages_history.db");
    }
}