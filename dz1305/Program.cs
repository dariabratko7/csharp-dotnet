using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//У системі доставки їжі є клас, який одночасно розраховує вартість замовлення, застосовує знижки, зберігає замовлення у файлі
//та надсилає push-повідомлення клієнту. Необхідно розподілити відповідальність між класами та зробити систему розширюваною для
//додавання нових способів повідомлень і нових типів знижок без зміни існуючого коду.

//Сам клас:

namespace FoodDeliveryRefactored
{
 

    public class Order
    {
        public string CustomerName { get; set; }
        public decimal Price { get; set; }

        public Order(string customerName, decimal price)
        {
            CustomerName = customerName;
            Price = price;
        }
    }
 

    public interface IPriceCalculator
    {
        decimal Calculate(decimal basePrice);
    }

    public class BasePriceCalculator : IPriceCalculator
    {
        public decimal Calculate(decimal basePrice)
        {
            return basePrice + 5;
        }
    }
 

    public interface IDiscount
    {
        decimal Apply(decimal price);
    }

    public class BigOrderDiscount : IDiscount
    {
        public decimal Apply(decimal price)
        {
            if (price > 50)
                return price - 10;

            return price;
        }
    }
 

    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public class FileOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            File.AppendAllText(
                "orders.txt",
                $"{order.CustomerName} - {order.Price}\n"
            );
        }
    }
 

    public interface INotificationService
    {
        void Notify(string customerName);
    }

    public class PushNotificationService : INotificationService
    {
        public void Notify(string customerName)
        {
            Console.WriteLine($"Push notification sent to {customerName}");
        }
    }
 

    public class FoodDeliveryService
    {
        private readonly IPriceCalculator priceCalculator;
        private readonly List<IDiscount> discounts;
        private readonly IOrderRepository repository;
        private readonly INotificationService notification;

        public FoodDeliveryService(
            IPriceCalculator priceCalculator,
            List<IDiscount> discounts,
            IOrderRepository repository,
            INotificationService notification)
        {
            this.priceCalculator = priceCalculator;
            this.discounts = discounts;
            this.repository = repository;
            this.notification = notification;
        }

        public void ProcessOrder(string customerName, decimal orderPrice)
        {
            decimal price = priceCalculator.Calculate(orderPrice);

            foreach (var discount in discounts)
            {
                price = discount.Apply(price);
            }

            var order = new Order(customerName, price);

            repository.Save(order);
            notification.Notify(customerName);
        }
    }
 

    class Program
    {
        static void Main(string[] args)
        {
            var service = new FoodDeliveryService(
                new BasePriceCalculator(),
                new List<IDiscount>
                {
                    new BigOrderDiscount()
                },
                new FileOrderRepository(),
                new PushNotificationService()
            );

            service.ProcessOrder("Ivan", 60);

            Console.WriteLine("Order processed successfully.");
        }
    }
}

//У грі існує загальний клас персонажа, від якого походять воїн, маг і лучник. Наразі кожен персонаж зобов’язаний реалізовувати методи
//використання меча, лука та магії, навіть якщо вони йому не підходять. Необхідно переробити архітектуру так, щоб класи залежали лише
//від потрібних їм можливостей і не містили зайвих методів.

//Сам клас:

namespace GameCharacters
{
    

    public interface ISwordUser
    {
        void UseSword();
    }

    public interface IBowUser
    {
        void UseBow();
    }

    public interface IMagicUser
    {
        void UseMagic();
    }
 

    public abstract class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
    }
 

    public class Warrior : Character, ISwordUser
    {
        public void UseSword()
        {
            Console.WriteLine("Warrior attacks with sword");
        }
    }
 
    public class Mage : Character, IMagicUser
    {
        public void UseMagic()
        {
            Console.WriteLine("Mage casts a spell");
        }
    }
 

    public class Archer : Character, IBowUser
    {
        public void UseBow()
        {
            Console.WriteLine("Archer shoots an arrow");
        }
    }
 

    class Program
    {
        static void Main(string[] args)
        {
            Warrior warrior = new Warrior { Name = "Conan", Health = 100 };
            Mage mage = new Mage { Name = "Gandalf", Health = 80 };
            Archer archer = new Archer { Name = "Legolas", Health = 90 };

            warrior.UseSword();
            mage.UseMagic();
            archer.UseBow();

            Console.WriteLine("Game simulation finished.");
        }
    }
}
