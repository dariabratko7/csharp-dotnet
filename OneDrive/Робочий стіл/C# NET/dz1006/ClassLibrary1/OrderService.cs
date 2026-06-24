using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopLibrary
{
    public class OrderService
    {
        public List<Order> GetAllOrders()
        {
            using var context = new ShopContext();

            return context.Orders.Include(o => o.Products).ToList();
        }

        public void CreateOrder(string customerName, List<int> productIds)
        {
            using var context = new ShopContext();

            var products = context.Products.Where(p => productIds.Contains(p.Id)).ToList();

            if (products.Count == 0)
            {
                Console.WriteLine("Не знайдено жодного продукту для замовлення!");
                return;
            }

            var newOrder = new Order
            {
                CustomerName = customerName,
                OrderDate = DateTime.Now,
                Products = products 
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();
            Console.WriteLine($"Замовлення для клієнта '{customerName}' успішно створено!");
        }

        public void DeleteOrder(int orderId)
        {
            using var context = new ShopContext();
            var order = context.Orders.Find(orderId);

            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
                Console.WriteLine($"Замовлення №{orderId} успішно видалено.");
            }
            else
            {
                Console.WriteLine($"Замовлення №{orderId} не знайдено в базі.");
            }
        }
    }
}