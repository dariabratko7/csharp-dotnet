using System;
using System.Collections.Generic;
using System.Linq;

//Створіть тип користувача Телефон.Потрібно зберігати таку інформацію: 

//•	Назва телефону 
//•	Виробник
//•	Ціна
//•	Дата випуску

//Для масиву телефонів виконайте такі завдання за допомогою агрегатних операцій з LINQ:

//1.	Прорахуйте кількість телефонів
//2.	Порахуйте кількість телефонів з ціною більше 100 
//3.	розрахуйте кількість телефонів з ціною в діапазоні від 400 до 700 
//4.	Прорахуйте кількість телефонів конкретного виробника
//5.	знайдіть телефон з мінімальною ціною
//6.	знайдіть телефон з максимальною ціною
//7.	Відобразіть інформацію про найстаріший телефон
//8.	Відобразіть інформацію про найсвіжіший телефон
//9.	знайдіть середню ціну телефону
//10.	Відобразіть п'ять найдорожчих телефонів 
//11.	Відобразіть п'ять найдешевших телефонів 
//12.	Відобразіть три найстаріші телефони 
//13.	Відобразіть три найновіші телефони

class Phone
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public double Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    public override string ToString()
    {
        return $"{Name} | {Manufacturer} | {Price}$ | {ReleaseDate.ToShortDateString()}";
    }
}

class Program
{
    static void Main()
    {
        List<Phone> phones = new List<Phone>()
        {
            new Phone { Name = "iPhone 15", Manufacturer = "Apple", Price = 1200, ReleaseDate = new DateTime(2023, 9, 22) },
            new Phone { Name = "Galaxy S23", Manufacturer = "Samsung", Price = 950, ReleaseDate = new DateTime(2023, 2, 1) },
            new Phone { Name = "Redmi Note 12", Manufacturer = "Xiaomi", Price = 300, ReleaseDate = new DateTime(2022, 10, 15) },
            new Phone { Name = "Pixel 8", Manufacturer = "Google", Price = 800, ReleaseDate = new DateTime(2023, 10, 10) },
            new Phone { Name = "Nokia 3310", Manufacturer = "Nokia", Price = 80, ReleaseDate = new DateTime(2000, 9, 1) },
            new Phone { Name = "iPhone 13", Manufacturer = "Apple", Price = 700, ReleaseDate = new DateTime(2021, 9, 24) },
            new Phone { Name = "Galaxy A54", Manufacturer = "Samsung", Price = 450, ReleaseDate = new DateTime(2023, 3, 15) },
            new Phone { Name = "Moto G60", Manufacturer = "Motorola", Price = 250, ReleaseDate = new DateTime(2021, 5, 10) }
        };

        Console.WriteLine("1. Кількість телефонів: " + phones.Count());

        Console.WriteLine("2. Телефонів дорожче 100$: " +
            phones.Count(p => p.Price > 100));

        Console.WriteLine("3. Телефонів з ціною 400-700$: " +
            phones.Count(p => p.Price >= 400 && p.Price <= 700));

        string manufacturer = "Apple";
        Console.WriteLine($"4. Телефонів виробника {manufacturer}: " +
            phones.Count(p => p.Manufacturer == manufacturer));

        Phone minPricePhone = phones.OrderBy(p => p.Price).First();
        Console.WriteLine("\n5. Найдешевший телефон:");
        Console.WriteLine(minPricePhone);

        Phone maxPricePhone = phones.OrderByDescending(p => p.Price).First();
        Console.WriteLine("\n6. Найдорожчий телефон:");
        Console.WriteLine(maxPricePhone);

        Phone oldestPhone = phones.OrderBy(p => p.ReleaseDate).First();
        Console.WriteLine("\n7. Найстаріший телефон:");
        Console.WriteLine(oldestPhone);

        Phone newestPhone = phones.OrderByDescending(p => p.ReleaseDate).First();
        Console.WriteLine("\n8. Найновіший телефон:");
        Console.WriteLine(newestPhone);

        double avgPrice = phones.Average(p => p.Price);
        Console.WriteLine("\n9. Середня ціна телефону: " + avgPrice);

        Console.WriteLine("\n10. П'ять найдорожчих телефонів:");
        var expensivePhones = phones
            .OrderByDescending(p => p.Price)
            .Take(5);

        foreach (var phone in expensivePhones)
        {
            Console.WriteLine(phone);
        }

        Console.WriteLine("\n11. П'ять найдешевших телефонів:");
        var cheapPhones = phones
            .OrderBy(p => p.Price)
            .Take(5);

        foreach (var phone in cheapPhones)
        {
            Console.WriteLine(phone);
        }

        Console.WriteLine("\n12. Три найстаріші телефони:");
        var oldestPhones = phones
            .OrderBy(p => p.ReleaseDate)
            .Take(3);

        foreach (var phone in oldestPhones)
        {
            Console.WriteLine(phone);
        }

        Console.WriteLine("\n13. Три найновіші телефони:");
        var newestPhones = phones
            .OrderByDescending(p => p.ReleaseDate)
            .Take(3);

        foreach (var phone in newestPhones)
        {
            Console.WriteLine(phone);
        }
    }
}


//друге завдання

class Program
{
    static void Main()
    {
        Person[] people = CreatePeople(30);
 
        var people25 = people.Where(p => p.Age >= 25);

        Console.WriteLine("1) Люди віком від 25 років:");
        foreach (var p in people25)
        {
            Console.WriteLine($"{p.Name} - {p.Age}");
        }
 
        var avgSalary = people.Average(p => p.Salary);

        Console.WriteLine($"\n2) Середня зарплата: {avgSalary}");
 
        var top5 = people
            .OrderByDescending(p => p.Salary)
            .Take(5);

        Console.WriteLine("\n3) Топ 5 зарплат:");
        foreach (var p in top5)
        {
            Console.WriteLine($"{p.Name} - {p.Salary}");
        }
        int wallmartCount = people.Count(p => p.Company.Name == "WallMart");

        Console.WriteLine($"\n4) Працівників WallMart: {wallmartCount}");
 
        var companies = people
            .Select(p => p.Company.Name)
            .Distinct();

        Console.WriteLine("\n5) Компанії:");
        foreach (var c in companies)
        {
            Console.WriteLine(c);
        }
 
        var manyCars = people.Where(p => p.Cars.Count > 2);

        Console.WriteLine("\n6) Люди з більше ніж 2 машинами:");
        foreach (var p in manyCars)
        {
            Console.WriteLine($"{p.Name} - {p.Cars.Count} машин");
        }
 
        var allCars = people
            .SelectMany(p => p.Cars)
            .Select(c => c.Model)
            .Distinct();

        Console.WriteLine("\n7) Усі моделі машин:");
        foreach (var car in allCars)
        {
            Console.WriteLine(car);
        }

        var maxPhones = people
            .OrderByDescending(p => p.PhoneNumbers.Count)
            .First();

        Console.WriteLine($"\n8) Найбільше номерів у: {maxPhones.Name}");
 
        var grouped = people.GroupBy(p => p.Company.Name);

        Console.WriteLine("\n9) Групування по компаніях:");
        foreach (var group in grouped)
        {
            Console.WriteLine($"\n{group.Key}");

            foreach (var person in group)
            {
                Console.WriteLine(person.Name);
            }
        }

        var hardWorkers = people
            .Where(p => p.WorkTimePerDay.TotalHours > 10);

        Console.WriteLine("\n10) Працюють понад 10 годин:");
        foreach (var p in hardWorkers)
        {
            Console.WriteLine($"{p.Name} - {p.WorkTimePerDay.TotalHours} год");
        }
    }

    static Person[] CreatePeople(int count = 10)
    {
        Person[] persons = new Person[count];
        Random r = new Random();

        string[] names =
        {
            "John", "Jane", "Alice", "Bob",
            "Charlie", "Emma", "Michael", "Olivia"
        };

        string[] companyNames =
        {
            "Wendys", "WallMart", "BestBuy", "Abbys"
        };

        string[] carModels =
        {
            "Toyota Camry",
            "Honda Accord",
            "BMW 3 Series",
            "Tesla Model 3"
        };

        for (int i = 0; i < persons.Length; i++)
        {
            List<Car> cars = new List<Car>();

            int carCount = r.Next(1, 5);

            for (int j = 0; j < carCount; j++)
            {
                cars.Add(new Car
                {
                    Model = carModels[r.Next(carModels.Length)]
                });
            }

            List<string> phones = new List<string>();

            int phoneCount = r.Next(1, 5);

            for (int j = 0; j < phoneCount; j++)
            {
                phones.Add("+380" + r.Next(100000000, 999999999));
            }

            persons[i] = new Person
            {
                Name = names[r.Next(names.Length)],
                Age = r.Next(18, 60),
                Salary = r.Next(1000, 15000),
                IsAdult = true,
                WorkTimePerDay = TimeSpan.FromHours(r.Next(4, 13)),
                Company = new Company
                {
                    Name = companyNames[r.Next(companyNames.Length)],
                    Address = "Main Street"
                },
                Cars = cars,
                PhoneNumbers = phones
            };
        }

        return persons;
    }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public bool IsAdult { get; set; }
    public TimeSpan WorkTimePerDay { get; set; }
    public Company Company { get; set; }
    public List<string> PhoneNumbers { get; set; }
    public List<Car> Cars { get; set; }
}

class Car
{
    public string Model { get; set; }
}

class Company
{
    public string Name { get; set; }
    public string Address { get; set; }
}