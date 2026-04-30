using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Розробити систему знижок для інтернет-магазину, де можна додавати різні умови знижок на товари. Для цього використовуйте делегати та анонімні методи.

//Ми маємо товари з цінами. У магазину є різні умови знижок (наприклад, знижка 10 % на товари дорожчі за 1000 грн, знижка 15 % на електроніку тощо).
//Користувач може додавати нові умови знижок у реальному часі, використовуючи делегати та анонімні методи. Магазин застосовує знижки та показує фінальну ціну кожного товару.

//Вимоги:

//1) Створити клас Product з назвою та ціною.
//2) Створити делегат DiscountRule, який приймає товар та повертає нову ціну.
//3) У Store додати масив знижкових правил (DiscountRule[]).
//4) Дати користувачеві можливість додавати свої правила знижок у процесі роботи програми (з використанням лямбда-виразів та анонімних методів).
//5) Застосувати всі знижки до товарів та вивести підсумкову ціну.

//public class Product
//{
//    public string Name { get; set; }
//    public double Price { get; set; }

//    public Product(string name, double price)
//    {
//        Name = name;
//        Price = price;
//    }
//}
  
//public delegate double DiscountRule(Product product);
 
//public class Store
//{
//    private List<DiscountRule> discountRules = new List<DiscountRule>();

//    public void AddRule(DiscountRule rule)
//    {
//        discountRules.Add(rule);
//    }

//    public double ApplyDiscounts(Product product)
//    {
//        double finalPrice = product.Price;

//        foreach (var rule in discountRules)
//        {
//            finalPrice = rule(new Product(product.Name, finalPrice));
//        }

//        return finalPrice;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        Store store = new Store();
 
//        store.AddRule(p => p.Price > 1000 ? p.Price * 0.9 : p.Price);
 
//        store.AddRule(delegate (Product p)
//        {
//            if (p.Name.ToLower().Contains("електроніка"))
//                return p.Price * 0.85;

//            return p.Price;
//        });
 
//        Console.WriteLine("Додати власну знижку? (y/n)");
//        if (Console.ReadLine() == "y")
//        {
//            Console.WriteLine("Введіть % знижки:");
//            double percent = Convert.ToDouble(Console.ReadLine());
 
//            store.AddRule(p => p.Price * (1 - percent / 100));
//        }
 
//        var products = new List<Product>
//        {
//            new Product("Електроніка: телефон", 1500),
//            new Product("Книга", 500),
//            new Product("Ноутбук", 2000)
//        };

//        Console.WriteLine("\nРезультати:");

//        foreach (var product in products)
//        {
//            double finalPrice = store.ApplyDiscounts(product);
//            Console.WriteLine($"{product.Name}: {product.Price} -> {finalPrice}");
//        }
//    }
//}

//Створіть узагальнений делегат із універсальним параметром, такого виду: 

//delegate T SomeOp<T>(T v);

//Створіть 2 методи, з значенням int, string, що повертається, і відповідно з одним аргументом int, string.
//    У методі int – підсумуйте всі цифри вашого числа, у методі string – переверніть рядок. Методи викликати через делегат.

//delegate T SomeOp<T>(T v);

//class Program
//{
//    static int SumDigits(int number)
//    {
//        int sum = 0;
//        number = Math.Abs(number);

//        while (number > 0)
//        {
//            sum += number % 10;
//            number /= 10;
//        }

//        return sum;
//    }

//    static string ReverseString(string str)
//    {
//        char[] chars = str.ToCharArray();
//        Array.Reverse(chars);
//        return new string(chars);
//    }

//    static void Main()
//    {
//        SomeOp<int> intOp = SumDigits;
//        int resultInt = intOp(12345);
//        Console.WriteLine("Сума цифр: " + resultInt);
 
//        SomeOp<string> strOp = ReverseString;
//        string resultStr = strOp("Hello");
//        Console.WriteLine("Рядок навпаки: " + resultStr);
//    }
//}

//Реалізуйте метод «Where», який як параметр приймає масив і делегат (предикат). За допомогою делегата,
//    користувач може задати умову, за якою він отримає елементи з методу. Наприклад, всі елементи, які поділяються на 2 без залишку або всіх користувачів віком від 35 років.

delegate bool Predicate<T>(T item);

class MyCollection
{ 
    public static T[] Where<T>(T[] array, Predicate<T> condition)
    {
        List<T> result = new List<T>();

        foreach (var item in array)
        {
            if (condition(item))
            {
                result.Add(item);
            }
        }

        return result.ToArray();
    }
}
 
class User
{
    public string Name { get; set; }
    public int Age { get; set; }

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main()
    { 
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        var evenNumbers = MyCollection.Where(numbers, x => x % 2 == 0);

        Console.WriteLine("Парні числа:");
        foreach (var n in evenNumbers)
            Console.Write(n + " ");

        Console.WriteLine();
 
        User[] users =
        {
            new User("Іван", 30),
            new User("Оля", 40),
            new User("Петро", 35)
        };

        var adults = MyCollection.Where(users, u => u.Age >= 35);

        Console.WriteLine("\nКористувачі 35+:");
        foreach (var u in adults)
            Console.WriteLine($"{u.Name} - {u.Age}");
    }
}