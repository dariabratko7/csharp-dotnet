using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        int port = 5000;
        TcpListener listener = new TcpListener(IPAddress.Any, port);

        listener.Start();
        Console.WriteLine($"[Сервер] Запущено на порту {port}. Очікування підключень...");

        while (true)
        {
            using TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("[Сервер] Клієнт підключився.");

            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            string? request = reader.ReadLine()?.Trim().ToLower();
            Console.WriteLine($"[Сервер] Отримано запит: '{request}'");

            string response = request switch
            {
                "time" or "час" => DateTime.Now.ToLongTimeString(),
                "date" or "дата" => DateTime.Now.ToShortDateString(),
                _ => "Помилка: невідомий запит. Використовуйте 'час' або 'дата'."
            };

            writer.WriteLine(response);
            Console.WriteLine($"[Сервер] Відповідь надіслано: {response}");
            Console.WriteLine("[Сервер] З'єднання з клієнтом закрито.\n");
        }
    }
}
