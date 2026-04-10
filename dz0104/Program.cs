using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz0104
{
    //Дано структуру даних із 5 туристичних путівок.Поля структури: Путівка, Місце відпочинку,
    //    Вартість, Період відпочинку. Отримати список місць відпочинку, виходячи з наявної суми грошей.
    //    Для відпочинку, використовувати TimeSpan. Усі поля структури, приватні, доступом до необхідних полів, реалізувати через метод.
    //struct Voucher
    //{
    //    private string name;
    //    private string location;
    //    private double price;
    //    private TimeSpan duration;

    //    public Voucher(string name, string location, double price, TimeSpan duration)
    //    {
    //        this.name = name;
    //        this.location = location;
    //        this.price = price;
    //        this.duration = duration;
    //    }

    //    public string GetLocation()
    //    {
    //        return location;
    //    }

    //    public double GetPrice()
    //    {
    //        return price;
    //    }

    //    public void Print()
    //    {
    //        Console.WriteLine($"Путівка: {name}, Місце: {location}, Вартість: {price}, Тривалість: {duration.Days} днів");
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        Voucher[] vouchers = new Voucher[5];

    //        vouchers[0] = new Voucher("Тур1", "Туреччина", 10000, new TimeSpan(7, 0, 0, 0));
    //        vouchers[1] = new Voucher("Тур2", "Єгипет", 8000, new TimeSpan(5, 0, 0, 0));
    //        vouchers[2] = new Voucher("Тур3", "Італія", 15000, new TimeSpan(10, 0, 0, 0));
    //        vouchers[3] = new Voucher("Тур4", "Болгарія", 6000, new TimeSpan(6, 0, 0, 0));
    //        vouchers[4] = new Voucher("Тур5", "Греція", 12000, new TimeSpan(8, 0, 0, 0));

    //        Console.Write("Введіть вашу суму: ");
    //        double money = Convert.ToDouble(Console.ReadLine());

    //        Console.WriteLine("\nДоступні місця відпочинку:");

    //        bool found = false;

    //        foreach (var v in vouchers)
    //        {
    //            if (v.GetPrice() <= money)
    //            {
    //                Console.WriteLine(v.GetLocation());
    //                found = true;
    //            }
    //        }

    //        if (!found)
    //        {
    //            Console.WriteLine("Немає доступних путівок 😢");
    //        }
    //    }
    //}

    //    Створити структуру Admin: id, name, roots(перерахування).
    //    Створити масив об'єктів класу, встановити кожному об'єкту, різну кількість повноважень.Зразковий висновок у методі Main:
    //Name - Alex, Roots: Create, Delete, Update.
    //Name - Marry, Roots: Create, Update.
    //Name - Bob, Roots: Create.

    //[Flags]
    //enum Roots
    //{
    //    None = 0,
    //    Create = 1,
    //    Delete = 2,
    //    Update = 4
    //}

    //struct Admin
    //{
    //    private int id;
    //    private string name;
    //    private Roots roots;

    //    public Admin(int id, string name, Roots roots)
    //    {
    //        this.id = id;
    //        this.name = name;
    //        this.roots = roots;
    //    }


    //    public void Print()
    //    {
    //        Console.WriteLine($"Name - {name}, Roots: {roots}");
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        Admin[] admins = new Admin[3];

    //        admins[0] = new Admin(1, "Alex", Roots.Create | Roots.Delete | Roots.Update);
    //        admins[1] = new Admin(2, "Marry", Roots.Create | Roots.Update);
    //        admins[2] = new Admin(3, "Bob", Roots.Create);

    //        foreach (var admin in admins)
    //        {
    //            admin.Print();
    //        }
    //    }
    //}

    //Створити структуру Студент, що містить поля: id, ім'я, прізвище, середній бал. 
    //    Визначити студентів з балом вище за середній. Користувач вводить дані про кількість студентів, їх прізвища,
    //    імена та бал для кожного. Програма повинна визначити середній бал та вивести прізвища та імена студентів, чий бал вищий за середній. 
    //    Використовувати масив структури на 5 студентів.
    //struct Student
    //{
    //    private int id;
    //    private string firstName;
    //    private string lastName;
    //    private double averageGrade;

    //    public Student(int id, string firstName, string lastName, double averageGrade)
    //    {
    //        this.id = id;
    //        this.firstName = firstName;
    //        this.lastName = lastName;
    //        this.averageGrade = averageGrade;

    //    public double GetAverageGrade()
    //    {
    //        return averageGrade;
    //    }

    //    public string GetFullName()
    //    {
    //        return lastName + " " + firstName;
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        Student[] students = new Student[5];

    //        Console.Write("Введіть кількість студентів (до 5): ");
    //        int n = Convert.ToInt32(Console.ReadLine());

    //        if (n > 5) n = 5;

    //        for (int i = 0; i < n; i++)
    //        {
    //            Console.WriteLine($"\nСтудент {i + 1}:");

    //            Console.Write("Ім'я: ");
    //            string firstName = Console.ReadLine();

    //            Console.Write("Прізвище: ");
    //            string lastName = Console.ReadLine();

    //            Console.Write("Середній бал: ");
    //            double grade = Convert.ToDouble(Console.ReadLine());

    //            students[i] = new Student(i + 1, firstName, lastName, grade);
    //        }

    //        double sum = 0;
    //        for (int i = 0; i < n; i++)
    //        {
    //            sum += students[i].GetAverageGrade();
    //        }

    //        double average = sum / n;

    //        Console.WriteLine($"\nСередній бал групи: {average:F2}");

    //        Console.WriteLine("\nСтуденти з балом вище середнього:");

    //        bool found = false;

    //        for (int i = 0; i < n; i++)
    //        {
    //            if (students[i].GetAverageGrade() > average)
    //            {
    //                Console.WriteLine(students[i].GetFullName());
    //                found = true;
    //            }
    //        }

    //        if (!found)
    //        {
    //            Console.WriteLine("Таких студентів немає");
    //        }
    //    }
    //}

    //Є інформація про N учасників спортивних змагань з п'ятиборства. 
    //    Про кожного учасника відома така інформація: прізвище, місце, яке займає по кожному з видів.
    //    Ввести інформацію щодо учасників змагань та вивести інформацію про спортсмена,
    //        який посів останнє місце за сумою місць у п'ятиборстві.Для поля “місце, 
    //        яке займає по кожному з видів” створити масив на 5 осередків.
    //    Усього призових місць для кожного із змагань – 5.
    //struct Athlete
    //{
    //    private string lastName;
    //    private int[] places; 

    //    public Athlete(string lastName, int[] places)
    //    {
    //        this.lastName = lastName;
    //        this.places = places;
    //    }

    //    public int GetTotalPlaces()
    //    {
    //        int sum = 0;

    //        for (int i = 0; i < places.Length; i++)
    //        {
    //            sum += places[i];
    //        }

    //        return sum;
    //    }

    //    public string GetLastName()
    //    {
    //        return lastName;
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        Console.Write("Введіть кількість учасників: ");
    //        int n = Convert.ToInt32(Console.ReadLine());

    //        Athlete[] athletes = new Athlete[n];

    //        for (int i = 0; i < n; i++)
    //        {
    //            Console.WriteLine($"\nСпортсмен {i + 1}");

    //            Console.Write("Прізвище: ");
    //            string lastName = Console.ReadLine();

    //            int[] places = new int[5];

    //            Console.WriteLine("Введіть місця у 5 видах спорту (від 1 до 5):");

    //            for (int j = 0; j < 5; j++)
    //            {
    //                Console.Write($"Вид {j + 1}: ");
    //                places[j] = Convert.ToInt32(Console.ReadLine());
    //            }

    //            athletes[i] = new Athlete(lastName, places);
    //        }

    //        int maxSum = athletes[0].GetTotalPlaces();
    //        int index = 0;

    //        for (int i = 1; i < n; i++)
    //        {
    //            int sum = athletes[i].GetTotalPlaces();

    //            if (sum > maxSum)
    //            {
    //                maxSum = sum;
    //                index = i;
    //            }
    //        }

    //        Console.WriteLine("\nСпортсмен, який посів останнє місце:");
    //        Console.WriteLine($"Прізвище: {athletes[index].GetLastName()}");
    //        Console.WriteLine($"Сума місць: {athletes[index].GetTotalPlaces()}");
    //    }
    //}

    //    Гра "Вишневий пиріг" У цю гру можуть грати два та більше гравців.Перед ними пиріг,
    //        розрізаний на рівні шматки: граючі самі визначають, наскільки шматків пиріг ріжеться 
    //        по горизонталі і скільки по вертикалі.У лівий верхній шматок запечена вишня.
    //    Гравці по черзі беруть шматок за шматком.Брати можна одразу по кілька шматків.
    //    Програє той, кому дістанеться шматок із вишнею (останній шматок).

    //class Program
    //{
    //    static void Main()
    //    {
    //        Console.Write("Введіть кількість рядків пирога: ");
    //        int rows = Convert.ToInt32(Console.ReadLine());

    //        Console.Write("Введіть кількість стовпців пирога: ");
    //        int cols = Convert.ToInt32(Console.ReadLine());

    //        bool[,] cake = new bool[rows, cols];

    //        for (int i = 0; i < rows; i++)
    //        {
    //            for (int j = 0; j < cols; j++)
    //            {
    //                cake[i, j] = true;
    //            }
    //        }

    //        int player = 1;

    //        while (cake[0, 0])
    //        {
    //            Console.WriteLine($"\nХід гравця {player}");

    //            Console.WriteLine("Візьми шматок (введи координати)");

    //            Console.Write("Рядок: ");
    //            int r = Convert.ToInt32(Console.ReadLine());

    //            Console.Write("Стовпець: ");
    //            int c = Convert.ToInt32(Console.ReadLine());

    //            if (r < 0 || r >= rows || c < 0 || c >= cols || !cake[r, c])
    //            {
    //                Console.WriteLine("Невірний хід! Спробуй ще раз.");
    //                continue;
    //            }

    //            cake[r, c] = false;

    //            if (r == 0 && c == 0)
    //            {
    //                Console.WriteLine($"\n🍒 Гравець {player} взяв вишню і ПРОГРАВ!");
    //                break;
    //            }

    //            player = (player == 1) ? 2 : 1;
    //        }
    //    }
    //}

    //Створити 2 об'єкти DateTime та отримати повний проміжок між датами: року, місяця, дні, години, хвилини, секунди. Вивести у форматі:
    //Мені 8886 днів 20 годин 51 хвилин
    //І
    //Мені 24 років 20 годин 52 хвилини

    //class Program
    //{
    //    static void Main()
    //    {
    //        DateTime date1 = new DateTime(2000, 1, 1, 10, 30, 0);
    //        DateTime date2 = DateTime.Now;

    //        TimeSpan diff = date2 - date1;

    //        Console.WriteLine($"Мені {diff.Days} днів {diff.Hours} годин {diff.Minutes} хвилин");

    //        int years = date2.Year - date1.Year;

    //        if (date2.Month < date1.Month ||
    //           (date2.Month == date1.Month && date2.Day < date1.Day))
    //        {
    //            years--;
    //        }

    //        DateTime temp = date1.AddYears(years);
    //        TimeSpan rest = date2 - temp;

    //        Console.WriteLine($"Мені {years} років {rest.Hours} годин {rest.Minutes} хвилини");
    //    }
    //}

    //    Вивчити усі модифікатори доступу.Зрозуміти, який і для чого використовується.
    //        Знайти застосування модифікаторів для своїх проектів чи завдань. 
    //Створити клас, що описує будь-який предмет і складається із 6 полів.Кожне 
    //        з полів має відмінний від іншого поля модифікатор доступу. Над кожним із полів,
    //у вигляді коментаря, написати, де це поле доступне, а де ні.

    class Book
    {

        public string Title;

        private string author;

        protected int pages;

        internal double price;

        protected internal string genre;

        private protected int year;

        public Book(string title, string author, int pages, double price, string genre, int year)
        {
            Title = title;
            this.author = author;
            this.pages = pages;
            this.price = price;
            this.genre = genre;
            this.year = year;
        }

        public void Print()
        {
            Console.WriteLine($"Назва: {Title}");
            Console.WriteLine($"Автор: {author}");
            Console.WriteLine($"Сторінки: {pages}");
            Console.WriteLine($"Ціна: {price}");
            Console.WriteLine($"Жанр: {genre}");
            Console.WriteLine($"Рік: {year}");
        }
    }
}
