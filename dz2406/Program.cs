using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=LibraryDb;Trusted_Connection=True;";

            using (var context = new LibraryContext())
            {
                context.Database.EnsureCreated();
                if (!context.Books.Any())
                {
                    SeedData(context);
                }
            }

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                LibraryManager.PrintBooksByAuthor(db, "Тарас Шевченко");

                LibraryManager.DeleteCheapestBookInCategory(db, 1);
            }

            Console.WriteLine("\nПрограма завершила роботу. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        public static void SeedData(LibraryContext context)
        {
            var author1 = new Author { Name = "Тарас Шевченко" };
            var author2 = new Author { Name = "Іван Франко" };

            var cat1 = new Category { Name = "Поезія" };
            var cat2 = new Category { Name = "Проза" };

            var book1 = new Book { Title = "Кобзар", Price = 120.00m, ReleaseDate = new DateTime(1840, 4, 18), Author = author1, Category = cat1 };
            var book2 = new Book { Title = "Захар Беркут", Price = 85.50m, ReleaseDate = new DateTime(1883, 1, 1), Author = author2, Category = cat2 };
            var book3 = new Book { Title = "Гайдамаки", Price = 45.00m, ReleaseDate = new DateTime(1841, 10, 1), Author = author1, Category = cat1 };

            context.Authors.AddRange(author1, author2);
            context.Categories.AddRange(cat1, cat2);
            context.Books.AddRange(book1, book2, book3);
            context.SaveChanges();
        }
    }
}