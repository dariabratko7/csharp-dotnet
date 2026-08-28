using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebPageDownloader;

public class WebPageProcessor
{
    private static readonly HttpClient httpClient = new HttpClient();
    private readonly string _storageDir;
    private readonly string _logFilePath;

    public WebPageProcessor()
    {
        _storageDir = Path.Combine(Directory.GetCurrentDirectory(), "DownloadedPages");
        Directory.CreateDirectory(_storageDir);

        _logFilePath = Path.Combine(_storageDir, "app_log.txt");

        if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }
    }

    public async Task<PageResult?> ProcessUrlAsync(string url)
    {
        try
        {
            await LogAsync($"[INFO] Початок завантаження: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string htmlContent = await response.Content.ReadAsStringAsync();
            long sizeInBytes = System.Text.Encoding.UTF8.GetByteCount(htmlContent);

            string safeFileName = Regex.Replace(url, @"[^\w\d]", "_") + ".html";
            string filePath = Path.Combine(_storageDir, safeFileName);
            await File.WriteAllTextAsync(filePath, htmlContent);

            await LogAsync($"[SUCCESS] Успішно збережено: {url} -> {filePath}");

            string cleanText = ExtractTextFromHtml(htmlContent);
            int wordCount = CountWords(cleanText);
            string keywords = ExtractKeywords(cleanText);

            return new PageResult
            {
                Url = url,
                DownloadedAt = DateTime.Now,
                FileSizeBytes = sizeInBytes,
                WordCount = wordCount,
                TopKeywords = keywords,
                LocalFilePath = filePath
            };
        }
        catch (Exception ex)
        {
            await LogAsync($"[ERROR] Помилка обробки {url}: {ex.Message}");
            Console.WriteLine($" Помилка завантаження {url}: {ex.Message}");
            return null;
        }
    }

    private string ExtractTextFromHtml(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        doc.DocumentNode.Descendants()
            .Where(n => n.Name == "script" || n.Name == "style")
            .ToList()
            .ForEach(n => n.Remove());

        return doc.DocumentNode.InnerText;
    }

    private int CountWords(string text)
    {
        var words = Regex.Matches(text, @"\b\w+\b");
        return words.Count;
    }

    private string ExtractKeywords(string text)
    {
        var words = Regex.Matches(text, @"\b[a-zA-Zа-яА-ЯіІїЇєЄ'{2,}]\b")
                         .Select(m => m.Value.ToLower())
                         .Where(w => w.Length > 3);

        var topWords = words.GroupBy(w => w)
                            .OrderByDescending(g => g.Count())
                            .Take(5)
                            .Select(g => $"{g.Key} ({g.Count()})");

        return string.Join(", ", topWords);
    }

    private async Task LogAsync(string message)
    {
        string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
        await File.AppendAllTextAsync(_logFilePath, logLine);
    }
}