using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz0304
{
    internal class Program
    {

            //            Розробіть систему керування інструментом для ігрової програми.У цій системі гравці можуть
            //                мати різні предмети(зброя, броню, зілля тощо), які вони можуть використовувати у грі.
            //                Кожен предмет має унікальні властивості, такі як назва, рівень, вартість, вага та ін.
            //Створіть клас «Item» (Предмет)із такими властивостями:
            //•	Name: назва предмета.
            //•	Level: рівень предмета.
            //•	Price: Вартість предмета. 
            //•	Weight: вага предмета.
            //Створіть клас «Player» (Гравець)із властивостями:
            //•	Name: ім'я гравця.
            //•	Inventory: колекція предметів, які гравець має у своєму інвентарі.
            //Використовуючи 2 властивості та 3 методи, визначте такі можливості:
            //            1) Додавання нових предметів до інвентарю гравця.
            //2) Видалення предметів із інвентарю гравця.
            //3) Отримання загальної ваги всіх предметів інвентарю гравця.
            //4) Отримання загальної вартості всіх предметів інвентарю гравця.
            //5) Пошук предметів певного рівня або з певною вартістю інвентарю гравця.

        //    public class Item
        //{
        //    public string Name { get; set; }
        //    public int Level { get; set; }
        //    public decimal Price { get; set; }
        //    public double Weight { get; set; }

        //    public Item(string name, int level, decimal price, double weight)
        //    {
        //        Name = name;
        //        Level = level;
        //        Price = price;
        //        Weight = weight;
        //    }
        //}

        //public class Player
        //{
        //    public string Name { get; set; }
        //    public List<Item> Inventory { get; set; } = new List<Item>();

        //    public Player(string name)
        //    {
        //        Name = name;
        //    }

        //    public void AddItem(Item item)
        //    {
        //        if (item != null)
        //        {
        //            Inventory.Add(item);
        //            Console.WriteLine($"✅ Предмет '{item.Name}' (рівень {item.Level}) додано до інвентарю гравця {Name}.");
        //        }
        //    }

        //    public void RemoveItem(string itemName)
        //    {
        //        var itemToRemove = Inventory.FirstOrDefault(i =>
        //            i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

        //        if (itemToRemove != null)
        //        {
        //            Inventory.Remove(itemToRemove);
        //            Console.WriteLine($"✅ Предмет '{itemName}' видалено з інвентарю гравця {Name}.");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"❌ Предмет '{itemName}' не знайдено в інвентарі.");
        //        }
        //    }

        //    public (double TotalWeight, decimal TotalPrice, List<Item> FoundItems) GetInventoryInfo(
        //        int? searchLevel = null,
        //        decimal? searchPrice = null)
        //    {
        //        double totalWeight = Inventory.Sum(item => item.Weight);
        //        decimal totalPrice = Inventory.Sum(item => item.Price);

        //        var query = Inventory.AsQueryable();
        //        if (searchLevel.HasValue)
        //            query = query.Where(i => i.Level == searchLevel.Value);
        //        if (searchPrice.HasValue)
        //            query = query.Where(i => i.Price == searchPrice.Value);

        //        List<Item> found = query.ToList();

        //        return (totalWeight, totalPrice, found);
        //    }
        //}

        //class Program1
        //{
        //    static void Main(string[] args)
        //    {
        //        Console.WriteLine("=== Система керування інвентарем гри (C#) ===\n");

        //        Player player = new Player("Артемій");

        //        Item sword = new Item("Меч лицаря", 10, 250.75m, 15.5);
        //        Item armor = new Item("Латна броня", 8, 180.0m, 25.0);
        //        Item potion = new Item("Зілля здоров'я", 1, 15.0m, 0.5);
        //        Item ring = new Item("Кільце сили", 15, 500.0m, 0.2);

        //        player.AddItem(sword);
        //        player.AddItem(armor);
        //        player.AddItem(potion);
        //        player.AddItem(ring);

        //        Console.WriteLine("\n--- Інвентар після додавання ---");


        //        var info = player.GetInventoryInfo();
        //        Console.WriteLine($"Загальна вага всіх предметів: {info.TotalWeight} кг");
        //        Console.WriteLine($"Загальна вартість всіх предметів: {info.TotalPrice} монет");

        //        var level10 = player.GetInventoryInfo(searchLevel: 10).FoundItems;
        //        Console.WriteLine("\n🔍 Предмети рівня 10:");
        //        foreach (var item in level10)
        //            Console.WriteLine($"   • {item.Name} (вартість {item.Price} монет, вага {item.Weight} кг)");

        //        var price15 = player.GetInventoryInfo(searchPrice: 15.0m).FoundItems;
        //        Console.WriteLine("\n🔍 Предмети вартістю 15 монет:");
        //        foreach (var item in price15)
        //            Console.WriteLine($"   • {item.Name} (рівень {item.Level}, вага {item.Weight} кг)");

        //        Console.WriteLine("\n--- Видалення предмета ---");
        //        player.RemoveItem("Зілля здоров'я");

        //        Console.WriteLine("\n--- Інвентар після видалення ---");
        //        var infoAfter = player.GetInventoryInfo();
        //        Console.WriteLine($"Загальна вага: {infoAfter.TotalWeight} кг");
        //        Console.WriteLine($"Загальна вартість: {infoAfter.TotalPrice} монет");

        //        Console.WriteLine("\n✅ Завдання виконано! Система повністю працює.");
        //        Console.ReadKey();
        //    }
        //}

        //Розробити власну бібліотеку Translater для імпорту в інші проекти.Суть бібліотеки – псевдо перекладач. 
        //    Створити клас з полями: слово англійською, його переклад російською та масив класу(початковий розмір на 5 значень).
        //    Користувач вводить слово російською, ви йому повертає переклад англійською.У класі реалізувати метод для розширення
        //   перекладача, для додавання 2 - х слів(англійське та російське),
        //    метод приймає 2 слова та додає їх у масив. Передбачити додавання перекладу слова, яке вже є у масиві.

        public class Translater
        {

            public string EnglishWord { get; set; }
            public string RussianWord { get; set; }
            public Translater[] Dictionary { get; private set; }

            private int count = 0;

            public Translater()
            {
                Dictionary = new Translater[5];
            }

            public void AddWords(string english, string russian)
            {
                for (int i = 0; i < count; i++)
                {
                    if (Dictionary[i] != null &&
                        Dictionary[i].RussianWord.Equals(russian, StringComparison.OrdinalIgnoreCase))
                    {
                        Dictionary[i].EnglishWord = english;
                        Console.WriteLine($"🔄 Оновлено переклад для '{russian}' → '{english}'");
                        return;
                    }
                }

                if (count == Dictionary.Length)
                {
                    ExpandDictionary();
                }
у
                Dictionary[count] = new Translater
                {
                    EnglishWord = english,
                    RussianWord = russian

                };
                count++;

                Console.WriteLine($"✅ Додано: {english} ↔ {russian}");
            }

            private void ExpandDictionary()
            {
                int newSize = Dictionary.Length * 2;
                Translater[] newDict = new Translater[newSize];

                Array.Copy(Dictionary, newDict, count);
                Dictionary = newDict;

                Console.WriteLine($"📈 Масив розширено до {newSize} елементів.");
            }

            public string TranslateToEnglish(string russianWord)
            {
                for (int i = 0; i < count; i++)
                {
                    if (Dictionary[i] != null &&
                        Dictionary[i].RussianWord.Equals(russianWord, StringComparison.OrdinalIgnoreCase))
                    {
                        return Dictionary[i].EnglishWord;
                    }
                }

                return "❌ Переклад не знайдено.";
            }

            public void ShowDictionary()
            {
                Console.WriteLine("\n📖 Поточний словник Translater:");
                for (int i = 0; i < count; i++)
                {
                    if (Dictionary[i] != null)
                    {
                        Console.WriteLine($"   {Dictionary[i].RussianWord} → {Dictionary[i].EnglishWord}");
                    }
                }
                Console.WriteLine($"   Використано слотів: {count} з {Dictionary.Length}\n");
            }
        }

        class Program2
        {
            static void Main(string[] args)
            {
                Console.WriteLine("=== Бібліотека Translater (псевдо-перекладач) ===\n");

                Translater translator = new Translater();

                translator.AddWords("hello", "привет");
                translator.AddWords("world", "мир");
                translator.AddWords("cat", "кот");
                translator.AddWords("dog", "собака");
                translator.AddWords("book", "книга");

                translator.AddWords("computer", "компьютер");

                Console.WriteLine("Переклад 'привет' → " + translator.TranslateToEnglish("привет"));
                Console.WriteLine("Переклад 'мир' → " + translator.TranslateToEnglish("мир"));
                Console.WriteLine("Переклад 'кот' → " + translator.TranslateToEnglish("кот"));

                translator.AddWords("hi", "привет");
                Console.WriteLine("Після оновлення 'привет' → " + translator.TranslateToEnglish("привет"));

                translator.ShowDictionary();

                Console.WriteLine("Введіть російське слово для перекладу (або 'вихід'): ");
                while (true)
                {
                    string input = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(input) || input.ToLower() == "вихід") break;

                    string result = translator.TranslateToEnglish(input);
                    Console.WriteLine($"Англійський переклад: {result}\n");
                }

                Console.WriteLine("✅ Бібліотека Translater готова!");
                Console.WriteLine("Щоб використати в іншому проекті:");
                Console.WriteLine("1. Створіть Class Library проект");
                Console.WriteLine("2. Скопіюйте клас Translater туди");
                Console.WriteLine("3. Зберіть DLL і додайте посилання в будь-який інший проект.");

                Console.ReadKey();
            }
        }
    }
    }
