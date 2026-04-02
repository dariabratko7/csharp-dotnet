using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz2803
{
    internal class Program
    {
        

            //Завдання 4 Створіть клас "Веб-сайт".Необхідно зберігати в полях класу: 
            //назву сайту, шлях до сайту, опис сайту, ір - адресу сайту.
            //Реалізуйте методи класу для введення даних, виведення даних, реалізуйте доступ до окремих полів через методи класу.

            class WebSite
        {
            private string name;
            private string url;
            private string description;
            private string ipAddress;
            

            public void InputData()
            {
                Console.Write("Введіть назву сайту:");
                name = Console.ReadLine();

                Console.Write("Введыть шлях до сайту:");
                url = Console.ReadLine();

                Console.Write("Введіть опис сайту:");
                description = Console.ReadLine();

                Console.Write("Введіть ір адресу");
                ipAddress = Console.ReadLine();
            }
            public void DisplayData()
            {
                Console.WriteLine("Назва сайту: " + name);
                Console.WriteLine("Шлях до сайту: " + url);
                Console.WriteLine("Опис сайту: " + description);
                Console.WriteLine("IP-адреса: " + ipAddress);
            }
            public string GetName() => name;
            public void SetName(string newName) => name = newName;

            public string GetUrl() => url;
            public void SetUrl(string newUrl) => url = newUrl;
        }
    }

    //Завдання 5 Створіть клас "Журнал".
    //    Необхідно зберігати в полях класу: назву журналу, рік заснування, опис журналу,
    //    контактний телефон, контактний email.Реалізуйте методи класу для введення даних, виведення даних,
    //    реалізуйте доступ до окремих полів через методи класу.

    class Magazine
    {
        private string title;
        private int year;
        private string description;
        private string phone;
        private string email;

        public void InputData()
        {
            Console.Write("Введіть назву журналу: ");
            title = Console.ReadLine();

            Console.Write("Введіть рік заснування: ");
            year = int.Parse(Console.ReadLine());

            Console.Write("Введіть опис журналу: ");
            description = Console.ReadLine();

            Console.Write("Введіть контактний телефон: ");
            phone = Console.ReadLine();

            Console.Write("Введіть контактний email: ");
            email = Console.ReadLine();
        }

        public void DisplayData()
        {
            Console.WriteLine("Назва журналу: " + title);
            Console.WriteLine("Рік заснування: " + year);
            Console.WriteLine("Опис: " + description);
            Console.WriteLine("Телефон: " + phone);
            Console.WriteLine("Email: " + email);
        }

        public string GetTitle() => title;
        public void SetTitle(string newTitle) => title = newTitle;
    }
}

//Завдання 6 Створіть клас "Магазин". Необхідно зберігати в полях класу: назву магазину,
//    адресу, опис профілю магазину, контактний телефон, контактний email. 
//    Реалізуйте методи класу для введення даних, виведення даних, реалізуйте доступ до окремих полів через методи класу.

class Store
{
    private string name;
    private string address;
    private string description;
    private string phone;
    private string email;

    public void InputData()
    {
        Console.Write("Введіть назву магазину: ");
        name = Console.ReadLine();

        Console.Write("Введіть адресу магазину: ");
        address = Console.ReadLine();

        Console.Write("Введіть опис профілю магазину: ");
        description = Console.ReadLine();

        Console.Write("Введіть контактний телефон: ");
        phone = Console.ReadLine();

        Console.Write("Введіть контактний email: ");
        email = Console.ReadLine();
    }

    public void DisplayData()
    {
        Console.WriteLine("Назва магазину: " + name);
        Console.WriteLine("Адреса: " + address);
        Console.WriteLine("Опис: " + description);
        Console.WriteLine("Телефон: " + phone);
        Console.WriteLine("Email: " + email);
    }

    public string GetName() => name;
    public void SetName(string newName) => name = newName;
}

//Завдання 7 Створіть клас "Резервуар". Реалізуйте перевантажені конструктори для створення резервуара з різними параметрами: 
//    тільки об'єм, об'єм і матеріал, об'єм, матеріал і поточний стан (заповнений або порожній). 
//    Реалізуйте методи для заповнення резервуара і спустошення його. Програма має коректно обробляти переповнення резервуара.

class Reservoir
{
    private double volume;
    private string material;
    private double currentLevel;

    public Reservoir(double volume)
    {
        this.volume = volume;
        this.material = "не вказано";
        this.currentLevel = 0;
    }

    public Reservoir(double volume, string material)
    {
        this.volume = volume;
        this.material = material;
        this.currentLevel = 0;
    }

    public Reservoir(double volume, string material, double currentLevel)
    {
        this.volume = volume;
        this.material = material;
        this.currentLevel = currentLevel > volume ? volume : currentLevel;
    }

    public void Fill(double amount)
    {
        if (currentLevel + amount > volume)
        {
            Console.WriteLine("Переповнення! Заповнено до максимуму.");
            currentLevel = volume;
        }
        else
        {
            currentLevel += amount;
            Console.WriteLine($"Заповнено {amount}. Поточний рівень: {currentLevel}");
        }
    }

    public void Empty(double amount)
    {
        if (currentLevel - amount < 0)
        {
            Console.WriteLine("Резервуар порожній.");
            currentLevel = 0;
        }
        else
        {
            currentLevel -= amount;
            Console.WriteLine($"Вилито {amount}. Поточний рівень: {currentLevel}");
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Об'єм: {volume}, Матеріал: {material}, Поточний рівень: {currentLevel}");

    }
}
