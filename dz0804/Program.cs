using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz0804
{
    //Розробити систему керування курсами на онлайн-платформі.У вас є базовий клас “Курс", 
    //який містить загальну інформацію про курс, таку як назва, опис, вартість і т.д. 
    //Цей клас також має двох спадкоємців: клас "Платний курс", який містить інформацію про те,
    //скільки коштує курс, та клас"Безкоштовний курс", який містить інформацію про те, що курс безкоштовний.
    //Ваше завдання -створити клас "Користувач", який містить інформацію про користувача платформи, включаючи ім'я,
    //електронну пошту та список курсів, які він проходить. Кожен елемент у списку має бути об'єктом типу
    //"Курс", але може бути як "Платним Курсом", так і "Безкоштовним Курсом".
    //Клас "Користувач" повинен мати метод"AddCourse", який прийматиме об'єкт типу "Курс" і додаватиме його до списку 
    //курсів користувача. Також він повинен мати метод"RemoveCourse", який прийматиме об'єкт типу "Курс"
    //та видалятиме його зі списку курсів користувача.
    //Крім того, клас "Користувач" повинен мати метод"GetTotalAmount", який повертатиме загальну вартість усіх платних курсів, які користувач проходить.


    class Course
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Course(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public virtual double GetPrice()
        {
            return 0;
        }
    }
    class PaidCourse : Course
    {
        public double Price { get; set; }

        public PaidCourse(string name, string description, double price)
            : base(name, description)
        {
            Price = price;
        }

        public override double GetPrice()
        {
            return Price;
        }
    }
    class FreeCourse : Course
    {
        public FreeCourse(string name, string description)
            : base(name, description)
        {
        }

        public override double GetPrice()
        {
            return 0;
        }
    }
    class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        private List<Course> courses = new List<Course>();

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            courses.Remove(course);
        }

        public double GetTotalAmount()
        {
            double sum = 0;

            foreach (Course c in courses)
            {
                sum += c.GetPrice();
            }

            return sum;
        }

        public void ShowCourses()
        {
            Console.WriteLine($"\nКористувач: {Name}");
            Console.WriteLine("Курси:");

            foreach (Course c in courses)
            {
                Console.WriteLine($"- {c.Name} ({c.GetPrice()} грн)");
            }
        }
    }
    class Program
    {
        static void Main()
        {
            User user = new User("Оля", "olya@mail.com");

            Course c1 = new PaidCourse("C# Basics", "Основи C#", 500);
            Course c2 = new FreeCourse("HTML Intro", "Основи HTML");
            Course c3 = new PaidCourse("OOP in C#", "ООП", 800);

            user.AddCourse(c1);
            user.AddCourse(c2);
            user.AddCourse(c3);

            user.ShowCourses();

            Console.WriteLine($"\nЗагальна сума: {user.GetTotalAmount()} грн");

            user.RemoveCourse(c1);

            Console.WriteLine("\nПісля видалення курсу:");
            user.ShowCourses();
            Console.WriteLine($"\nНова сума: {user.GetTotalAmount()} грн");
        }
    }

    //Визначити ієрархію літаків.Створити авіакомпанію.Підрахувати загальну кількість місць та вантажопідйомність літаків.
    //        Провести сортування літаків за довжиною польоту.
    //        Знайти літак у компанії, що відповідає діапазону параметрів витрати бензину.Використовувати успадкування.

    class Airplane
    {
        public string Name { get; set; }
        public int Seats { get; set; }
        public double CargoCapacity { get; set; } 
        public double Range { get; set; } 
        public double FuelConsumption { get; set; }  

        public Airplane(string name, int seats, double cargo, double range, double fuel)
        {
            Name = name;
            Seats = seats;
            CargoCapacity = cargo;
            Range = range;
            FuelConsumption = fuel;
        }

        public override string ToString()
        {
            return $"{Name} | Місця: {Seats}, Вантаж: {CargoCapacity} кг, Дальність: {Range} км, Паливо: {FuelConsumption} л/100км";
        }
    }
    class PassengerPlane : Airplane
    {
        public PassengerPlane(string name, int seats, double range, double fuel)
            : base(name, seats, 0, range, fuel)
        {
        }
    }
    class CargoPlane : Airplane
    {
        public CargoPlane(string name, double cargo, double range, double fuel)
            : base(name, 0, cargo, range, fuel)
        {
        }
    }
    class Airline
    {
        private List<Airplane> planes = new List<Airplane>();

        public void AddPlane(Airplane plane)
        {
            planes.Add(plane);
        }

        public void ShowAll()
        {
            Console.WriteLine("\n✈️ Літаки компанії:");
            foreach (var p in planes)
                Console.WriteLine(p);
        }

        public int GetTotalSeats()
        {
            int sum = 0;
            foreach (var p in planes)
                sum += p.Seats;
            return sum;
        }

        public double GetTotalCargo()
        {
            double sum = 0;
            foreach (var p in planes)
                sum += p.CargoCapacity;
            return sum;
        }

        public void SortByRange()
        {
            planes = planes.OrderBy(p => p.Range).ToList();
        }
        public List<Airplane> FindByFuelConsumption(double min, double max)
        {
            return planes
                .Where(p => p.FuelConsumption >= min && p.FuelConsumption <= max)
                .ToList();
        }
    }
    class Program1
    {
        static void Main()
        {
            Airline airline = new Airline();

            airline.AddPlane(new PassengerPlane("Boeing 737", 180, 3000, 5.5));
            airline.AddPlane(new PassengerPlane("Airbus A320", 150, 3200, 5.2));
            airline.AddPlane(new CargoPlane("Boeing 747F", 70000, 8000, 12.0));
            airline.AddPlane(new CargoPlane("Antonov An-124", 120000, 7500, 14.5));

            airline.ShowAll();

            Console.WriteLine($"\n Загальна кількість місць: {airline.GetTotalSeats()}");
            Console.WriteLine($" Загальна вантажопідйомність: {airline.GetTotalCargo()} кг");

            airline.SortByRange();
            Console.WriteLine("\n Після сортування за дальністю:");
            airline.ShowAll();

            Console.WriteLine("\n Літаки з витратою 5–10 л/100км:");
            var result = airline.FindByFuelConsumption(5, 10);

            foreach (var p in result)
                Console.WriteLine(p);
        }
    }
}
