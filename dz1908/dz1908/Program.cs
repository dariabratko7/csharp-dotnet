using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace dz1908
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string apiKey = LoadApiKey();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("[Помилка]: Не вдалося зчитати API ключ з файлу appsettings.json!");
                return;
            }

            using (HttpClient httpClient = new HttpClient())
            {
                Console.WriteLine("=== ПРОГРАМА ПОШУКУ ФІЛЬМІВ (TMDB API) ===");

                while (true)
                {
                    Console.Write("\nВведіть назву фільму (або 'exit' для виходу): ");
                    string query = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(query))
                        continue;

                    if (query.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Роботу завершено.");
                        break;
                    }

                    await SearchMovieAsync(httpClient, apiKey, query);
                }
            }
        }

        private static string LoadApiKey()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration config = builder.Build();
                return config["TmdbApiKey"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Помилка читання конфігурації]: {ex.Message}");
                return null;
            }
        }

        private static async Task SearchMovieAsync(HttpClient client, string apiKey, string movieTitle)
        {
            string url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(movieTitle)}&language=uk-UA";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    if ((int)response.StatusCode == 401)
                        Console.WriteLine("[Помилка 401]: Невірний або некоректний API-ключ!");
                    else if ((int)response.StatusCode == 404)
                        Console.WriteLine("[Помилка 404]: Ресурс не знайдено.");
                    else
                        Console.WriteLine($"[Помилка HTTP]: Статус {response.StatusCode}");

                    return;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                MovieSearchResponse searchData = JsonSerializer.Deserialize<MovieSearchResponse>(jsonResponse);

                if (searchData?.Results == null || !searchData.Results.Any())
                {
                    Console.WriteLine("Фільмів за таким запитом не знайдено.");
                    return;
                }

                var movie = searchData.Results.First();

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine($"Назва:          {movie.Title}");
                Console.WriteLine($"Дата виходу:    {(string.IsNullOrEmpty(movie.ReleaseDate) ? "Невідомо" : movie.ReleaseDate)}");
                Console.WriteLine($"Рейтинг TMDB:   {movie.VoteAverage} / 10");
                Console.WriteLine($"Опис:           {(string.IsNullOrEmpty(movie.Overview) ? "Опис відсутній" : movie.Overview)}");
                Console.WriteLine("----------------------------------------");
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("\n[Помилка мережі]: Не вдалося з'єднатися з сервером TMDB. Перевірте підключення до інтернету.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Помилка]: {ex.Message}");
            }
        }
    }
}