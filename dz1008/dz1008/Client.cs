using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        string host = "127.0.0.1";
        int port = 5000;

        Console.WriteLine("Оберіть, що потрібно запросити у сервера:");
        Console.WriteLine("1 — Поточний час (введіть 'час' або 'time')");
        Console.WriteLine("2 — Поточна дата (введіть 'дата' або 'date')");
        Console.Write("Ваш вибір: ");

        string? userChoice = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userChoice))
        {
            Console.WriteLine("Вибір не може бути порожнім.");
            return;
        }

        try
        {
            using TcpClient client = new TcpClient(host, port);
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            writer.WriteLine(userChoice);

            string? response = reader.ReadLine();
            Console.WriteLine($"\n[Отримано від сервера]: {response}");
        }
        catch (SocketException)
        {
            Console.WriteLine("\nНе вдалося підключитися до сервера. Перевірте, чи сервер запущений.");
        }
    }
}
