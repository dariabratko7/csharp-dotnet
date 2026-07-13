using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace LibraryApp
{
    public class LibraryManager
    {
        public static void PrintBooksByAuthor(IDbConnection db, string authorName)
        {
            string sql = @"
                SELECT b.Title, a.Name AS AuthorName, c.Name AS CategoryName, b.Price, b.ReleaseDate
                FROM Books b
                JOIN Authors a ON b.AuthorId = a.Id
                JOIN Categories c ON b.CategoryId = c.Id
                WHERE a.Name LIKE @AuthorName";

            var books = db.Query(sql, new { AuthorName = "%" + authorName + "%" });

            Console.WriteLine($"\n--- 1) Книги автора: {authorName} ---");
            foreach (var b in books)
            {
                Console.WriteLine($"Назва: \"{b.Title}\" | Автор: {b.AuthorName} | Категорія: {b.CategoryName} | Ціна: {b.Price} $ | Дата: {((DateTime)b.ReleaseDate).ToShortDateString()}");
            }
        }

        public static void DeleteCheapestBookInCategory(IDbConnection db, int categoryId)
        {
            string selectSql = @"
                SELECT TOP 1 Title, Price FROM Books 
                WHERE CategoryId = @CategoryId 
                ORDER BY Price ASC";

            var cheapestBook = db.QueryFirstOrDefault(selectSql, new { CategoryId = categoryId });

            Console.WriteLine($"\n--- 2) Видалення найдешевшої книги в категорії ID {categoryId} ---");

            if (cheapestBook != null)
            {
                string deleteSql = @"
                    DELETE FROM Books 
                    WHERE Id = (
                        SELECT TOP 1 Id FROM Books 
                        WHERE CategoryId = @CategoryId 
                        ORDER BY Price ASC
                    )";

                db.Execute(deleteSql, new { CategoryId = categoryId });
                Console.WriteLine($"Книгу \"{cheapestBook.Title}\" ({cheapestBook.Price} $) успішно видалено через Dapper.");
            }
            else
            {
                Console.WriteLine("Книг у цій категорії не знайдено.");
            }
        }

        public static void IncreaseAllPricesByFivePercent(IDbConnection db)
        {
            string sql = "UPDATE Books SET Price = Price * 1.05";

            int affectedRows = db.Execute(sql);
            Console.WriteLine($"\n--- 3) Ціни всіх книг успішно збільшено на 5% (Оновлено книг: {affectedRows}) ---");
        }

        public static void PrintBooksInPriceRange(IDbConnection db, decimal minPrice, decimal maxPrice)
        {
            string sql = @"
                SELECT b.Title, b.Price, a.Name AS AuthorName 
                FROM Books b
                JOIN Authors a ON b.AuthorId = a.Id
                WHERE b.Price BETWEEN @MinPrice AND @MaxPrice
                ORDER BY b.Price";

            var books = db.Query(sql, new { MinPrice = minPrice, MaxPrice = maxPrice });

            Console.WriteLine($"\n--- 4) Книги в діапазоні цін від {minPrice} до {maxPrice} $ ---");
            foreach (var b in books)
            {
                Console.WriteLine($"- \"{b.Title}\" (Автор: {b.AuthorName}) — Ціна: {b.Price} $");
            }
        }

        public static void PrintAuthorsBookCount(IDbConnection db)
        {
            string sql = @"
                SELECT a.Id AS AuthorId, a.Name AS AuthorName, COUNT(b.Id) AS BooksCount
                FROM Authors a
                LEFT JOIN Books b ON a.Id = b.AuthorId
                GROUP BY a.Id, a.Name";

            var authorsStats = db.Query(sql);

            Console.WriteLine("\n--- 5) Кількість книг кожного автора (Анонімна колекція) ---");
            foreach (var stat in authorsStats)
            {
                Console.WriteLine($"ID: {stat.AuthorId} | Ім'я: {stat.AuthorName} | Книг у базі: {stat.BooksCount}");
            }
        }
    }
}