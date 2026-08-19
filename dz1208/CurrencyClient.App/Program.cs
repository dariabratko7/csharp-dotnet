using System;
using System.Threading.Tasks;

namespace CurrencyClient.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Currency Exchange Client";

            using ClientEngine client = new ClientEngine();

            try
            {
                Console.WriteLine("Підключення до сервера...");
                await client.ConnectAsync("127.0.0.1", 5000);
                Console.WriteLine("Успішно підключено!\n");

                while (true)
                {
                    Console.Write("Введіть пару валют (наприклад, USD EUR) або 'exit' для виходу: ");
                    string input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input)) continue;
                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                    string[] currencies = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (currencies.Length != 2)
                    {
                        Console.WriteLine(">> Помилка: Введіть саме ДВІ валюти через пробіл.\n");
                        continue;
                    }

                    string result = await client.GetExchangeRateAsync(currencies[0], currencies[1]);
                    Console.WriteLine($">> Відповідь сервера: {result}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА МЕРЕЖІ]: {ex.Message}");
            }
            finally
            {
                client.Disconnect();
                Console.WriteLine("Сеанс завершено. Натисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }
    }
}