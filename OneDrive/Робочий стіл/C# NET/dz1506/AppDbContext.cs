using dz1605;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace dz1506
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Dz1506Db;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("StoreProducts");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(p => p.Name)
                    .IsUnique();

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(10,2)");

                entity.Property(p => p.StockQuantity)
                    .HasDefaultValue(0);

                entity.Property(p => p.Description)
                    .IsRequired(false);

                entity.Ignore(p => p.TemporaryData);

            });
        }
    }
}