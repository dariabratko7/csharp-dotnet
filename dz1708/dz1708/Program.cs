using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

class Server
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        int port = 5000;

        var priceList = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "процесор", "12 500 грн" },
            { "відеокарта", "28 000 грн" },
            { "оперативна пам'ять", "3 200 грн" },
            { "материнська плата", "6 800 грн" },
            { "ssd", "2 500 грн" },
            { "блок живлення", "4 100 грн" }
        };

        using (UdpClient udpServer = new UdpClient(port))
        {
            Console.WriteLine($"[СЕРВЕР] Запущено на порту {port}. Очікування запитів...");

            while (true)
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

                byte[] receiveBytes = udpServer.Receive(ref clientEndPoint);
                string request = Encoding.UTF8.GetString(receiveBytes).Trim();

                Console.WriteLine($"[СЕРВЕР] Отримано запит від {clientEndPoint}: '{request}'");

                string response;
                if (priceList.TryGetValue(request, out string price))
                {
                    response = $"Ціна на '{request}': {price}";
                }
                else
                {
                    response = $"Товар '{request}' не знайдено у прайс-листі.";
                }

                byte[] sendBytes = Encoding.UTF8.GetBytes(response);
                udpServer.Send(sendBytes, sendBytes.Length, clientEndPoint);
            }
        }
    }
}