using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TplUrlDownloader
{
    class Program
    { 
        private static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
             
            List<string> urls = new List<string>
            {
                "https://httpbin.org/delay/1",
                "https://httpbin.org/delay/2",
                "https://httpbin.org/delay/3",
                "https://httpbin.org/delay/4",
                "https://httpbin.org/delay/5"
            };
             
            using CancellationTokenSource cts = new CancellationTokenSource();
             
            _ = Task.Run(() =>
            {
                Console.WriteLine("--> Натисніть БУДЬ-ЯКУ клавішу у будь-який момент, щоб СКАСУАТИ операцію...\n");
                Console.ReadKey(intercept: true);
                Console.WriteLine("\n[УВАГА] Отримано сигнал скасування від користувача!");
                 
                cts.Cancel();
            });

            Console.WriteLine("=== ПОЧАТОК ПАРАЛЕЛЬНОГО ЗАВАНТАЖЕННЯ (TPL) ===\n");

            try
            { 
                ParallelOptions parallelOptions = new ParallelOptions
                {
                    CancellationToken = cts.Token,       
                    MaxDegreeOfParallelism = 3         
                };

                await Parallel.ForEachAsync(urls, parallelOptions, async (url, token) =>
                {
                    await DownloadPageAsync(url, token);
                });

                Console.WriteLine("\n=== УСІ ЗАВАНТАЖЕННЯ УСПІШНО ЗАВЕРШЕНО ===");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\n[СКАСОВАНО] Операцію завантаження було перервано.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА] Виникла виняткова ситуація: {ex.Message}");
            }
        }

        private static async Task DownloadPageAsync(string url, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[ПОЧАТОК] Завантаження: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);
            string content = await response.Content.ReadAsStringAsync(cancellationToken);

            Console.WriteLine($"[УСПІХ]  {url} — Завантажено {content.Length} символів.");
        }
    }
}