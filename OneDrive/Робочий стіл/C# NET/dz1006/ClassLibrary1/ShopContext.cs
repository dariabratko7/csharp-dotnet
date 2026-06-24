using Microsoft.EntityFrameworkCore;

namespace ShopLibrary
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Ноутбук", Price = 35000 },
                new Product { Id = 2, Name = "Мишка", Price = 800 },
                new Product { Id = 3, Name = "Клавіатура", Price = 1500 }
            );
        }
    }
}