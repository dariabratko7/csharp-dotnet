using System;
using System.Collections.Generic;
using ShopLibrary;

namespace dz1006
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            OrderService orderService = new OrderService();

            Console.WriteLine("=== ЕТАП 1: Створення нових замовлень ===");

            orderService.CreateOrder("Дмитро Бондар", new List<int> { 1, 2 });

            orderService.CreateOrder("Олена Шевченко", new List<int> { 3 });

            Console.WriteLine("\n=== ЕТАП 2: Перегляд поточних замовлень ===");
            PrintAllOrders(orderService);

            Console.WriteLine("\n=== ЕТАП 3: Видалення замовлення ===");
            orderService.DeleteOrder(2);

            Console.WriteLine("\n=== ЕТАП 4: Перегляд замовлень після видалення ===");
            PrintAllOrders(orderService);

            Console.WriteLine("\nТестування успішно завершено! Натисніть будь-яку клавішу для виходу.");
            Console.ReadKey();
        }

        static void PrintAllOrders(OrderService service)
        {
            var orders = service.GetAllOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("У базі даних немає жодного замовлення.");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine($"Замовлення №{order.Id} від {order.OrderDate.ToShortDateString()} | Клієнт: {order.CustomerName}");
                Console.WriteLine("Товари в цьому замовленні:");

                foreach (var product in order.Products)
                {
                    Console.WriteLine($"  - {product.Name} ({product.Price} грн)");
                }
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}