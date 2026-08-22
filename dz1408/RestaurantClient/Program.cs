using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class OrderRequest
    {
        public string Action { get; set; }
        public string OrderId { get; set; }
        public string Items { get; set; }
        public int EstimatedMinutes { get; set; }
    }

    public class OrderResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public int QueuePosition { get; set; }
        public double RemainingMinutes { get; set; }
    }

    class Program
    {
        private const string ServerIp = "127.0.0.1";
        private const int ServerPort = 8888;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ РЕСТОРАНУ ===");
                Console.WriteLine("1. Створити нове замовлення");
                Console.WriteLine("2. Перевірити стан замовлення");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть опцію: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        await CreateOrderAsync();
                        break;
                    case "2":
                        await CheckStatusAsync();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private static async Task CreateOrderAsync()
        {
            Console.Write("Введіть перелік страв: ");
            string items = Console.ReadLine();

            Console.Write("Очікуваний час приготування (в хвилинах): ");
            int.TryParse(Console.ReadLine(), out int minutes);

            var req = new OrderRequest
            {
                Action = "CREATE",
                Items = items,
                EstimatedMinutes = minutes > 0 ? minutes : 2
            };

            var res = await SendServerRequestAsync(req);
            if (res != null && res.Success)
            {
                Console.WriteLine($"\n[УСПІХ] {res.Message}");
                Console.WriteLine($"ВАШ ID ЗАМОВЛЕННЯ: {res.OrderId} (запом'ятайте його для перевірки)");
            }
            else
            {
                Console.WriteLine($"\n[ПОМИЛКА] {res?.Message ?? "Не вдалося з'єднатися з сервером."}");
            }
        }

        private static async Task CheckStatusAsync()
        {
            Console.Write("Введіть ID вашого замовлення: ");
            string orderId = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(orderId))
            {
                Console.WriteLine("ID не може бути порожнім.");
                return;
            }

            var req = new OrderRequest
            {
                Action = "STATUS",
                OrderId = orderId
            };

            var res = await SendServerRequestAsync(req);
            if (res != null && res.Success)
            {
                Console.WriteLine($"\n--- Стан замовлення #{res.OrderId} ---");
                Console.WriteLine($"Статус: {res.Status}");

                if (res.Status == "COMPLETED")
                {
                    Console.WriteLine("Замовлення повністю готове!");
                }
                else
                {
                    Console.WriteLine($"Позиція в черзі: {res.QueuePosition}");
                    Console.WriteLine($"Залишилося часу (приблизно): {res.RemainingMinutes} хв.");
                }
            }
            else
            {
                Console.WriteLine($"\n[ПОМИЛКА] {res?.Message ?? "Замовлення не знайдено або помилка мережі."}");
            }
        }

        private static async Task<OrderResponse> SendServerRequestAsync(OrderRequest request)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(ServerIp, ServerPort);
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                    {
                        string jsonRequest = JsonSerializer.Serialize(request);
                        await writer.WriteLineAsync(jsonRequest);

                        string jsonResponse = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(jsonResponse)) return null;

                        return JsonSerializer.Deserialize<OrderResponse>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення: {ex.Message}");
                return null;
            }
        }
    }
}