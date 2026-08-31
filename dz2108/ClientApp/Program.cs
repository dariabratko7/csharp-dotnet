using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp
{
    public class UserRegistrationModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string serverUrl = "https://localhost:7123/api/auth/register";

            var newUser = new UserRegistrationModel
            {
                Username = "DariaB",
                Email = "daria@example.com",
                Password = "MySecurePassword123"
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Надсилання POST-запиту для реєстрації користувача...");

                    string jsonPayload = JsonSerializer.Serialize(newUser);

                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(serverUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"\n[УСПІХ (Статус {response.StatusCode})]:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        Console.WriteLine($"\n[ПОМИЛКА (Статус {response.StatusCode})]:");
                        Console.WriteLine(responseBody);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"\n[Помилка мережі]: Не вдалося з'єднатися з сервером. {ex.Message}");
                    Console.WriteLine("Перевірте, чи запущено проєкт сервера dz2108!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Помилка]: {ex.Message}");
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}