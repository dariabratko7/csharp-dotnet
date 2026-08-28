using System;

namespace WebPageDownloader;

public class PageResult
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime DownloadedAt { get; set; }
    public long FileSizeBytes { get; set; }
    public int WordCount { get; set; }
    public string TopKeywords { get; set; } = string.Empty;
    public string LocalFilePath { get; set; } = string.Empty;
}