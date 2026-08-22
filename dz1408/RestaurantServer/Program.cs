using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantServer
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

    public class OrderItem
    {
        public string OrderId { get; set; }
        public string Items { get; set; }
        public string Status { get; set; }  
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }

    class Program
    {
        private static ConcurrentQueue<OrderItem> orderQueue = new ConcurrentQueue<OrderItem>();
        private static List<OrderItem> allOrders = new List<OrderItem>();
        private static readonly object lockObj = new object();
        private static OrderItem currentOrder = null;

        static async Task Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            Console.WriteLine("=== Сервер ресторану запущено (Port 8888) ===");
             
            Task.Run(() => ProcessOrdersLoop());

            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
            {
                string jsonRequest = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(jsonRequest)) return;

                var request = JsonSerializer.Deserialize<OrderRequest>(jsonRequest);
                OrderResponse response = new OrderResponse();

                if (request.Action == "CREATE")
                {
                    string newId = Guid.NewGuid().ToString().Substring(0, 8);
                    var newOrder = new OrderItem
                    {
                        OrderId = newId,
                        Items = request.Items,
                        Status = "PENDING",
                        DurationMinutes = request.EstimatedMinutes > 0 ? request.EstimatedMinutes : 2
                    };

                    lock (lockObj)
                    {
                        allOrders.Add(newOrder);
                    }
                    orderQueue.Enqueue(newOrder);

                    response.Success = true;
                    response.OrderId = newId;
                    response.Message = "Замовлення успішно додано до черги.";
                    Console.WriteLine($"[НОВЕ ЗАМОВЛЕННЯ] ID: {newId} | Деталі: {request.Items}");
                }
                else if (request.Action == "STATUS")
                {
                    lock (lockObj)
                    {
                        var order = allOrders.FirstOrDefault(o => o.OrderId == request.OrderId);
                        if (order == null)
                        {
                            response.Success = false;
                            response.Message = "Замовлення з таким ID не знайдено.";
                        }
                        else
                        {
                            response.Success = true;
                            response.OrderId = order.OrderId;
                            response.Status = order.Status;

                            if (order.Status == "COMPLETED")
                            {
                                response.QueuePosition = 0;
                                response.RemainingMinutes = 0;
                            }
                            else if (order.Status == "IN_PROGRESS")
                            {
                                response.QueuePosition = 1;
                                double elapsed = (DateTime.Now - order.StartTime).TotalMinutes;
                                response.RemainingMinutes = Math.Max(0, Math.Round(order.DurationMinutes - elapsed, 1));
                            }
                            else  
                            {
                                var queueList = orderQueue.ToList();
                                int pos = queueList.FindIndex(o => o.OrderId == order.OrderId);
                                response.QueuePosition = (pos >= 0 ? pos + 1 : 0) + (currentOrder != null ? 1 : 0);

                                double currentRemaining = currentOrder != null
                                    ? Math.Max(0, currentOrder.DurationMinutes - (DateTime.Now - currentOrder.StartTime).TotalMinutes)
                                    : 0;
                                double priorWait = queueList.Take(pos >= 0 ? pos : 0).Sum(o => o.DurationMinutes);

                                response.RemainingMinutes = Math.Round(currentRemaining + priorWait + order.DurationMinutes, 1);
                            }
                        }
                    }
                }

                string jsonResponse = JsonSerializer.Serialize(response);
                await writer.WriteLineAsync(jsonResponse);
            }
        }

        private static void ProcessOrdersLoop()
        {
            while (true)
            {
                if (orderQueue.TryDequeue(out OrderItem order))
                {
                    lock (lockObj)
                    {
                        currentOrder = order;
                        order.Status = "IN_PROGRESS";
                        order.StartTime = DateTime.Now;
                    }

                    Console.WriteLine($"[В ОБРОБЦІ] Почато готування ID: {order.OrderId} (Час: {order.DurationMinutes} хв)");

                    Thread.Sleep(order.DurationMinutes * 5000);

                    lock (lockObj)
                    {
                        order.Status = "COMPLETED";
                        currentOrder = null;
                    }

                    Console.WriteLine($"[ГОТОВО] Замовлення ID: {order.OrderId} виконано!");
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}