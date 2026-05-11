using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Створіть клас «Товар», що містить властивості: ім'я, ціна, рейтинг. 
//Створіть клас «Категорія», що містить властивості: ім'я та масив товарів. 

//Створіть кілька об'єктів класу «Категорія». Відобразіть товари через об'єкт класу «Категорія». 

//Створіть клас «Кошик», що містить масив куплених товарів. 
//Створіть клас «User», що містить логін, пароль та об'єкт класу «Кошик». 

//Створіть об'єкт User. Відобразіть товари, які користувач додав до кошика. При додаванні товарів до кошика викликайте подію, що виводять список товарів, доданих до кошика. 


namespace OnlineStore
{
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }

        public Product(string name, double price, double rating)
        {
            Name = name;
            Price = price;
            Rating = rating;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Товар: {Name}, Ціна: {Price} грн, Рейтинг: {Rating}");
        }
    }

    class Category
    {
        public string Name { get; set; }
        public Product[] Products { get; set; }

        public Category(string name, Product[] products)
        {
            Name = name;
            Products = products;
        }

        public void ShowProducts()
        {
            Console.WriteLine($"\nКатегорія: {Name}");

            foreach (Product product in Products)
            {
                product.ShowInfo();
            }
        }
    }

    class Basket
    {
        public Product[] BoughtProducts = new Product[10];
        private int count = 0;

        public delegate void ProductAdded(Product[] products, int count);

        public event ProductAdded OnProductAdded;

        public void AddProduct(Product product)
        {
            if (count < BoughtProducts.Length)
            {
                BoughtProducts[count] = product;
                count++;

                OnProductAdded?.Invoke(BoughtProducts, count);
            }
            else
            {
                Console.WriteLine("Кошик заповнений!");
            }
        }

        public void ShowBasket()
        {
            Console.WriteLine("\nТовари у кошику:");

            for (int i = 0; i < count; i++)
            {
                BoughtProducts[i].ShowInfo();
            }
        }
    }

    class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Basket Basket { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
            Basket = new Basket();
        }
    }

    class Program
    {
        static void ShowAddedProducts(Product[] products, int count)
        {
            Console.WriteLine("\nПодія: товар додано до кошика!");
            Console.WriteLine("Список товарів у кошику:");

            for (int i = 0; i < count; i++)
            {
                products[i].ShowInfo();
            }
        }

        static void Main(string[] args)
        {
            Product p1 = new Product("Ноутбук", 35000, 4.8);
            Product p2 = new Product("Мишка", 800, 4.5);
            Product p3 = new Product("Клавіатура", 1500, 4.7);

            Product p4 = new Product("Телефон", 25000, 4.9);
            Product p5 = new Product("Навушники", 3000, 4.6);

            Category electronics = new Category(
                "Електроніка",
                new Product[] { p1, p2, p3 }
            );

            Category gadgets = new Category(
                "Гаджети",
                new Product[] { p4, p5 }
            );

            electronics.ShowProducts();
            gadgets.ShowProducts();

            User user = new User("admin", "12345");

            user.Basket.OnProductAdded += ShowAddedProducts;

            user.Basket.AddProduct(p1);
            user.Basket.AddProduct(p5);

            user.Basket.ShowBasket();

            Console.ReadKey();
        }
    }
}

//Припустимо, у вас є список рядків і ви хочете відсортувати їх за довжиною. Ви можете використовувати лямбда-вираз для створення функції, яка повертає довжину рядка,
//    а потім використовувати метод розширення OrderBy() для застосування цієї функції до кожного елемента списку та сортування їх за довжиною.

namespace LambdaSortExample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string>()
            {
                "apple",
                "cat",
                "elephant",
                "dog",
                "programming",
                "sun"
            };

            Console.WriteLine("Список до сортування:");
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }

            var sortedWords = words.OrderBy(word => word.Length);

            Console.WriteLine("\nСписок після сортування за довжиною:");
            foreach (string word in sortedWords)
            {
                Console.WriteLine(word);
            }

            Console.ReadKey();
        }
    }
}