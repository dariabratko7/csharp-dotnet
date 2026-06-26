using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "John Doe", Email = "john@example.com", IsActive = true, RegistrationDate = new DateTime(2023, 5, 10), BirthDate = new DateTime(1990, 3, 15) },
            new User { Id = 2, Name = "Alice Smith", Email = "alice@example.com", IsActive = true, RegistrationDate = new DateTime(2022, 8, 21), BirthDate = new DateTime(1995, 7, 20) },
            new User { Id = 3, Name = "Michael Johnson", Email = "michael@example.com", IsActive = false, RegistrationDate = new DateTime(2021, 2, 5), BirthDate = new DateTime(1985, 11, 30) }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, UserId = 1, OrderDate = new DateTime(2024, 1, 10), TotalAmount = 120.50m },
            new Order { Id = 2, UserId = 2, OrderDate = new DateTime(2024, 2, 15), TotalAmount = 250.00m },
            new Order { Id = 3, UserId = 1, OrderDate = new DateTime(2024, 3, 5), TotalAmount = 75.30m }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Dell XPS 13 Laptop", Price = 1200.99m },
            new Product { Id = 2, Name = "Logitech MX Master 3 Mouse", Price = 99.99m },
            new Product { Id = 3, Name = "LG UltraGear 27\" Monitor", Price = 399.99m },
            new Product { Id = 4, Name = "Razer BlackWidow Keyboard", Price = 149.50m }
        );

        base.OnModelCreating(modelBuilder);
    }
}