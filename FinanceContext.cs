using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PersonalFinanceApp
{
    public class FinanceContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ProfileSetting> ProfileSettings { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileSetting>()
                .HasOne(p => p.User)
                .WithOne(u => u.ProfileSetting)
                .HasForeignKey<ProfileSetting>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Продукти" },
                new Category { Id = 2, Name = "Зарплата" },
                new Category { Id = 3, Name = "Розваги" },
                new Category { Id = 4, Name = "Комунальні" }
            );
        }
    }
}