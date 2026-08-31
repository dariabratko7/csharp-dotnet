using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

class Client
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        string serverIp = "127.0.0.1"; 
        int serverPort = 5000;

        using (UdpClient udpClient = new UdpClient())
        {
            udpClient.Connect(serverIp, serverPort);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("--- КЛІЄНТ ПОШУКУ ЦІН КОМПЛЕКТУЮЧИХ ---");
            Console.WriteLine("Введіть назву запчастини (наприклад: процесор, відеокарта, ssd).");
            Console.WriteLine("Для виходу введіть 'exit'.\n");

            while (true)
            {
                Console.Write("Введіть запит: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Завершення роботи...");
                    break;
                }

                byte[] sendBytes = Encoding.UTF8.GetBytes(input);
                udpClient.Send(sendBytes, sendBytes.Length);
                
                byte[] receiveBytes = udpClient.Receive(ref remoteEndPoint);
                string response = Encoding.UTF8.GetString(receiveBytes);

                Console.WriteLine($"[ВІДПОВІДЬ СЕРВЕРА]: {response}\n");
            }
        }
    }
}