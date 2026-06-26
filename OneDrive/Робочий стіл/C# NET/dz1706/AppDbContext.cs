using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CompanyProjectsDb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Company) 
            .WithMany(c => c.Employees) 
            .HasForeignKey(e => e.CompanyId) 
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Projects)     
            .WithMany(p => p.Employees); 

        base.OnModelCreating(modelBuilder);
    }
}