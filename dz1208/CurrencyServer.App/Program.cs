using System;
using System.Threading.Tasks;

namespace CurrencyServer.App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Currency Exchange Server";

            ServerEngine server = new ServerEngine(port: 5000);

            server.OnLog += msg => Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");

            Task serverTask = server.StartAsync();

            Console.WriteLine("Натисніть 'Q' для зупинки сервера.\n");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    server.Stop();
                    break;
                }
            }

            await serverTask;
        }
    }
}