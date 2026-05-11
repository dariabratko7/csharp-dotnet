using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//Потрібно змоделювати роботу виробничого конвеєра, який виготовляє деталі. Конвеєр працює безперервно, використовуючи
//певний запас матеріалів, але в процесі роботи можуть виникати дві проблеми: закінчення матеріалів або поломка обладнання. 

//У системі мають бути реалізовані такі компоненти: Конвеєр - основний клас, який відповідає за процес виробництва деталей.
//Він поступово витрачає матеріали під час роботи. Якщо матеріали закінчуються, конвеєр не може продовжувати виробництво і
//має повідомити про необхідність завантаження нової партії. Також під час роботи конвеєр може випадково зламатися з певною ймовірністю. 

//Навантажувач - клас, який відповідає за постачання матеріалів. Коли конвеєр повідомляє, що матеріали закінчилися,
//навантажувач завантажує нову партію деталей і передає їх конвеєру. 

//Механік - інтерфейс, який описує поведінку об’єкта, що може ремонтувати конвеєр. Інтерфейс має містити метод для
//виконання ремонту та подію для повідомлення про процес ремонту. Потрібно створити клас (наприклад, MechanicHandler), який реалізує цей інтерфейс.

//У програмі необхідно реалізувати використання подій: 

//1) Якщо у конвеєра закінчилися матеріали, він генерує подію, після якої навантажувач завантажує нову партію. 
//2) Якщо конвеєр ламається (випадковим чином), генерується подія, після якої механік виконує ремонт. 

//Усі основні дії (виробництво, завантаження матеріалів, ремонт) мають супроводжуватися повідомленнями, які виводяться на екран через обробники подій.

namespace FactoryConveyor
{
    interface IMechanic
    {
        void Repair();

        event Action<string> RepairEvent;
    }

    class MechanicHandler : IMechanic
    {
        public event Action<string> RepairEvent;

        public void Repair()
        {
            RepairEvent?.Invoke("Механік почав ремонт конвеєра...");
            Thread.Sleep(2000);

            RepairEvent?.Invoke("Конвеєр успішно відремонтований!");
        }
    }

    class Loader
    {
        public void LoadMaterials(Conveyor conveyor)
        {
            Console.WriteLine("Навантажувач завантажує нову партію матеріалів...");
            Thread.Sleep(1500);

            conveyor.Materials += 10;

            Console.WriteLine("Матеріали успішно завантажені!");
        }
    }

    class Conveyor
    {
        public int Materials { get; set; }

        private bool isBroken = false;

        Random random = new Random();

        public event Action MaterialsEnded;

        public event Action Broken;

        public Conveyor(int materials)
        {
            Materials = materials;
        }

        public void Start()
        {
            Console.WriteLine("Конвеєр запущений!\n");

            for (int i = 1; i <= 20; i++)
            {
                if (random.Next(1, 11) == 3)
                {
                    isBroken = true;

                    Console.WriteLine("\nУвага! Конвеєр зламався!");
                    Broken?.Invoke();

                    isBroken = false;
                }

                if (Materials <= 0)
                {
                    Console.WriteLine("\nМатеріали закінчилися!");
                    MaterialsEnded?.Invoke();
                }

                if (!isBroken && Materials > 0)
                {
                    Console.WriteLine($"Виготовлено деталь №{i}");
                    Materials--;

                    Console.WriteLine($"Залишок матеріалів: {Materials}\n");
                }

                Thread.Sleep(1000);
            }

            Console.WriteLine("Роботу конвеєра завершено.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Conveyor conveyor = new Conveyor(5);

            Loader loader = new Loader();

            MechanicHandler mechanic = new MechanicHandler();

            conveyor.MaterialsEnded += () =>
            {
                loader.LoadMaterials(conveyor);
            };

            conveyor.Broken += () =>
            {
                mechanic.Repair();
            };

            mechanic.RepairEvent += (message) =>
            {
                Console.WriteLine(message);
            };

            conveyor.Start();

            Console.ReadKey();
        }
    }
}

//Створити інтерфейс «ICipher», який визначає методи підтримки шифрування рядків. В інтерфейсі оголошуються два методи
//Encode() і Decode(), які використовуються для шифрування та дешифрування рядків, відповідно. 
//Визначити клас, що реалізує інтерфейс, виконати шифрування та розшифрування одного рядка.

namespace CipherExample
{
    interface ICipher
    {
        string Encode(string text);
        string Decode(string text);
    }

    class CaesarCipher : ICipher
    {
        private int shift = 3;

        public string Encode(string text)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (char)(text[i] + shift);
            }

            return new string(result);
        }

        public string Decode(string text)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (char)(text[i] - shift);
            }

            return new string(result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ICipher cipher = new CaesarCipher();

            string originalText = "Hello World";

            string encoded = cipher.Encode(originalText);

            string decoded = cipher.Decode(encoded);

            Console.WriteLine($"Початковий рядок: {originalText}");
            Console.WriteLine($"Зашифрований рядок: {encoded}");
            Console.WriteLine($"Розшифрований рядок: {decoded}");

            Console.ReadKey();
        }
    }
}