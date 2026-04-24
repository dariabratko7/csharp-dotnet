using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz1004
{
    //    Створіть абстрактний базовий клас «Вантажоперевізник» і похідні класи «Літак», «Поїзд», «Автомобіль». 
    //Визначте час та вартість перевезення для зазначених міст та відстаней.Всього 3 напрямки:
    //Дніпро – Київ (592 км)
    //Запоріжжя – Дніпро(87 км)
    //Дніпро – Харків(218 км.). 
    //При створенні об'єкта одного з класів, консольна програма має виглядати схожим чином:
    //Виберіть тип транспорту:
    //1. Авто
    //2. Поїзд
    //3. Літак
    //1
    //Виберіть напрямок:
    //1.From - Дніпро, To - Київ, Distance - 592.
    //2.From - Запоріжжя, To - Дніпро, Distance - 87.
    //3.From - Дніпро, To - Харків, Distance - 218.
    //1
    //Вартість поїздки -2516
    //Тривалість поїздки -06:57:00

    //abstract class Vantazhopereneznyk
    //{
    //    public double Speed { get; set; }
    //    public double PricePerKm { get; set; }

    //    public abstract double CalculateTime(double distance);
    //    public abstract double CalculateCost(double distance);
    //}

    //class Car : Vantazhopereneznyk
    //{
    //    public Car()
    //    {
    //        Speed = 85;
    //        PricePerKm = 4.25;
    //    }

    //    public override double CalculateTime(double distance)
    //    {
    //        return distance / Speed;
    //    }

    //    public override double CalculateCost(double distance)
    //    {
    //        return distance * PricePerKm;
    //    }
    //}

    //class Train : Vantazhopereneznyk
    //{
    //    public Train()
    //    {
    //        Speed = 90;
    //        PricePerKm = 3.5;
    //    }

    //    public override double CalculateTime(double distance)
    //    {
    //        return distance / Speed;
    //    }

    //    public override double CalculateCost(double distance)
    //    {
    //        return distance * PricePerKm;
    //    }
    //}

    //class Plane : Vantazhopereneznyk
    //{
    //    public Plane()
    //    {
    //        Speed = 600;
    //        PricePerKm = 10;
    //    }

    //    public override double CalculateTime(double distance)
    //    {
    //        return distance / Speed;
    //    }

    //    public override double CalculateCost(double distance)
    //    {
    //        return distance * PricePerKm;
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        Console.WriteLine("Виберіть тип транспорту:");
    //        Console.WriteLine("1. Авто");
    //        Console.WriteLine("2. Поїзд");
    //        Console.WriteLine("3. Літак");

    //        int transportChoice = int.Parse(Console.ReadLine());

    //        Vantazhopereneznyk transport = null;

    //        switch (transportChoice)
    //        {
    //            case 1:
    //                transport = new Car();
    //                break;
    //            case 2:
    //                transport = new Train();
    //                break;
    //            case 3:
    //                transport = new Plane();
    //                break;
    //            default:
    //                Console.WriteLine("Невірний вибір!");
    //                return;
    //        }

    //        Console.WriteLine("\nВиберіть напрямок:");
    //        Console.WriteLine("1.From - Дніпро, To - Київ, Distance - 592.");
    //        Console.WriteLine("2.From - Запоріжжя, To - Дніпро, Distance - 87.");
    //        Console.WriteLine("3.From - Дніпро, To - Харків, Distance - 218.");

    //        int directionChoice = int.Parse(Console.ReadLine());

    //        double distance = 0;

    //        switch (directionChoice)
    //        {
    //            case 1:
    //                distance = 592;
    //                break;
    //            case 2:
    //                distance = 87;
    //                break;
    //            case 3:
    //                distance = 218;
    //                break;
    //            default:
    //                Console.WriteLine("Невірний вибір!");
    //                return;
    //        }

    //        double cost = transport.CalculateCost(distance);
    //        double hours = transport.CalculateTime(distance);

    //        TimeSpan time = TimeSpan.FromHours(hours);

    //        Console.WriteLine($"\nВартість поїздки - {cost:F0}");
    //        Console.WriteLine($"Тривалість поїздки - {time}");
    //    }
    //}

    //    Створіть базовий клас «Музичний інструмент» та похідні класи «Скрипка»,
    //    «Тромбон», «Укулеле», «Віолончель». За допомогою конструктора встановити ім'я кожного музичного інструменту та його характеристики. 
    //Реалізуйте для кожного класу методи: 
    //■ Sound — видає звук музичного інструменту(пишемо текстом у консоль); 
    //■ Show — відображає назву музичного інструменту; 
    //■ Desc — відображає опис музичного інструменту; 
    //■ History — відображає історію створення музичного інструменту.

    class MuzychnyiInstrument
    {
        protected string Name;
        protected string Description;

        public MuzychnyiInstrument(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual void Sound()
        {
            Console.WriteLine("Інструмент видає звук");
        }

        public virtual void Show()
        {
            Console.WriteLine($"Назва: {Name}");
        }

        public virtual void Desc()
        {
            Console.WriteLine($"Опис: {Description}");
        }

        public virtual void History()
        {
            Console.WriteLine("Історія інструменту невідома");
        }
    }

    class Skrypka : MuzychnyiInstrument
    {
        public Skrypka() : base("Скрипка", "Струнний смичковий інструмент")
        {
        }

        public override void Sound()
        {
            Console.WriteLine("Скрипка: ніжний високий звук");
        }

        public override void History()
        {
            Console.WriteLine("Скрипка з'явилась у XVI столітті в Італії");
        }
    }

    class Trombon : MuzychnyiInstrument
    {
        public Trombon() : base("Тромбон", "Мідний духовий інструмент")
        {
        }

        public override void Sound()
        {
            Console.WriteLine("Тромбон: гучний та потужний звук");
        }

        public override void History()
        {
            Console.WriteLine("Тромбон виник у XV столітті в Європі");
        }
    }

    class Ukulele : MuzychnyiInstrument
    {
        public Ukulele() : base("Укулеле", "Маленький струнний інструмент")
        {
        }

        public override void Sound()
        {
            Console.WriteLine("Укулеле: веселий дзвінкий звук");
        }

        public override void History()
        {
            Console.WriteLine("Укулеле походить з Гаваїв, XIX століття");
        }
    }

    class Violonchel : MuzychnyiInstrument
    {
        public Violonchel() : base("Віолончель", "Низький струнний смичковий інструмент")
        {
        }

        public override void Sound()
        {
            Console.WriteLine("Віолончель: глибокий, м'який звук");
        }

        public override void History()
        {
            Console.WriteLine("Віолончель виникла у XVI столітті в Італії");
        }
    }

    class Program
    {
        static void Main()
        {
            MuzychnyiInstrument[] instruments =
            {
            new Skrypka(),
            new Trombon(),
            new Ukulele(),
            new Violonchel()
        };

            foreach (var instrument in instruments)
            {
                Console.WriteLine("-----");
                instrument.Show();
                instrument.Desc();
                instrument.Sound();
                instrument.History();
            }
        }
    }

}
