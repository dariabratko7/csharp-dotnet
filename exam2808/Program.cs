using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebPageDownloader;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Асинхронний завантажувач та аналізатор веб-сторінок ===\n");

        using (var db = new AppDbContext())
        {
            await db.Database.EnsureCreatedAsync();
        }

        Console.WriteLine("Введіть URL-адреси (по одній на рядок). Коли закінчите — просто натисніть Enter на порожньому рядку:\n");
        List<string> urls = new List<string>();

        while (true)
        {
            Console.Write("URL: ");
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) break;

            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {
                urls.Add(input);
            }
            else
            {
                Console.WriteLine(" Некоректний URL! Приклад правильно формату: https://example.com");
            }
        }

        if (urls.Count == 0)
        {
            Console.WriteLine("\nНе вказано жодної URL-адреси. Завершення роботи.");
            return;
        }

        Console.WriteLine($"\n Початок паралельної обробки {urls.Count} сторінок...\n");

        var processor = new WebPageProcessor();
         
        Task<PageResult?>[] tasks = urls.Select(url => processor.ProcessUrlAsync(url)).ToArray();
        PageResult?[] results = await Task.WhenAll(tasks);
         
        using (var db = new AppDbContext())
        {
            foreach (var result in results)
            {
                if (result != null)
                {
                    db.PageResults.Add(result);
                }
            }
            await db.SaveChangesAsync();
        }
         
        Console.WriteLine("\n================ Результати Аналізу ================");
        foreach (var res in results.Where(r => r != null))
        {
            Console.WriteLine($" URL: {res!.Url}");
            Console.WriteLine($"    Файл: {res.LocalFilePath}");
            Console.WriteLine($"    Розмір: {res.FileSizeBytes / 1024.0:F2} KB");
            Console.WriteLine($"    Кількість слів: {res.WordCount}");
            Console.WriteLine($"    Топ-слова: {res.TopKeywords}");
            Console.WriteLine(new string('-', 50));
        }

        Console.WriteLine("\n Усі завдання виконано! Дані збережено у базу SQLite.");
    }
}