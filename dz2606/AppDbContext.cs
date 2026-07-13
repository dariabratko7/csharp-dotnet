using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace ShopParallelism
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductHistory> ProductHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackHistory();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void TrackHistory()
        {
            var entries = ChangeTracker.Entries<Product>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                var history = new ProductHistory
                {
                    ProductId = entry.Entity.Id,
                    Name = entry.Entity.Name,
                    Description = entry.Entity.Description,
                    Price = entry.Entity.Price,
                    AvailableQuantity = entry.Entity.AvailableQuantity,
                    ChangedAt = DateTime.UtcNow,
                    Action = entry.State.ToString()
                };

                ProductHistories.Add(history);
            }
        }
    }
}