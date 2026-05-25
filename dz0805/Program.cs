using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Реалізуйте консольну програму на C#, що моделює бій між командами персонажів, в якому обов'язково використовуються абстрактні класи,
//інтерфейси та успадкування. В основі системи має лежати абстрактний клас Character, який містить ім'я, здоров'я, силу атаки та абстрактний метод спеціальної дії.

//Від цього класу повинні успадковуватися кілька конкретних класів персонажів, наприклад воїн, лучник, маг і лікар, кожен з яких
//перевизначає поведінку атаки та реалізує унікальне спеціальне вміння.

//Додатково потрібно створити кілька інтерфейсів, таких як IHealer для лікування IRanged для далеких атак в IBuffer для посилення
//союзників і реалізувати їх тільки в тих класах, яким вони логічно підходять.

//Лікар має вміти лікувати союзників через інтерфейс IHealer, лучник -атакувати на відстані через IRanged, а маг - посилювати союзників через IBuffer. 

//У програмі потрібно створити дві команди персонажів та організувати покрокову симуляцію бою, де кожен живий персонаж по черзі
//або атакує супротивника, або використовує спеціальне вміння. Логіка бою повинна враховувати поточне здоров'я персонажів та завершуватись, коли одна з команд повністю переможена.

abstract class Character
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }

    public bool IsAlive => Health > 0;

    public Character(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
    }

    public virtual void Attack(Character target)
    {
        if (!IsAlive) return;

        target.Health -= AttackPower;

        Console.WriteLine($"{Name} атакує {target.Name} на {AttackPower} шкоди");

        if (target.Health < 0)
            target.Health = 0;

        Console.WriteLine($"{target.Name} HP: {target.Health}");
    }

    public abstract void SpecialAction(List<Character> allies, List<Character> enemies);
}

interface IHealer
{
    void Heal(Character target);
}

interface IRanged
{
    void RangedAttack(Character target);
}

interface IBuffer
{
    void Buff(Character target);
}

class Warrior : Character
{
    public Warrior(string name)
        : base(name, 150, 25)
    {
    }

    public override void SpecialAction(List<Character> allies, List<Character> enemies)
    {
        var enemy = enemies.FirstOrDefault(e => e.IsAlive);

        if (enemy != null)
        {
            Console.WriteLine($"{Name} використовує сильний удар!");

            enemy.Health -= AttackPower * 2;

            if (enemy.Health < 0)
                enemy.Health = 0;

            Console.WriteLine($"{enemy.Name} HP: {enemy.Health}");
        }
    }
}

class Archer : Character, IRanged
{
    public Archer(string name)
        : base(name, 100, 20)
    {
    }

    public void RangedAttack(Character target)
    {
        Console.WriteLine($"{Name} стріляє здалеку в {target.Name}");

        target.Health -= AttackPower + 10;

        if (target.Health < 0)
            target.Health = 0;

        Console.WriteLine($"{target.Name} HP: {target.Health}");
    }

    public override void SpecialAction(List<Character> allies, List<Character> enemies)
    {
        var enemy = enemies.LastOrDefault(e => e.IsAlive);

        if (enemy != null)
        {
            RangedAttack(enemy);
        }
    }
}

class Mage : Character, IBuffer
{
    public Mage(string name)
        : base(name, 90, 30)
    {
    }

    public void Buff(Character target)
    {
        target.AttackPower += 5;

        Console.WriteLine($"{Name} посилює {target.Name}");
    }

    public override void SpecialAction(List<Character> allies, List<Character> enemies)
    {
        var ally = allies
            .Where(a => a.IsAlive)
            .OrderBy(a => a.AttackPower)
            .FirstOrDefault();

        if (ally != null)
        {
            Buff(ally);
        }
    }
}

class Healer : Character, IHealer
{
    public Healer(string name)
        : base(name, 80, 10)
    {
    }

    public void Heal(Character target)
    {
        target.Health += 25;

        Console.WriteLine($"{Name} лікує {target.Name}");
        Console.WriteLine($"{target.Name} HP: {target.Health}");
    }

    public override void SpecialAction(List<Character> allies, List<Character> enemies)
    {
        var ally = allies
            .Where(a => a.IsAlive)
            .OrderBy(a => a.Health)
            .FirstOrDefault();

        if (ally != null)
        {
            Heal(ally);
        }
    }
}

class Program
{
    static void Main()
    {
        List<Character> team1 = new List<Character>()
        {
            new Warrior("Thor"),
            new Archer("Legolas"),
            new Mage("Merlin"),
            new Healer("Ariel")
        };

        List<Character> team2 = new List<Character>()
        {
            new Warrior("Kratos"),
            new Archer("Robin"),
            new Mage("Gandalf"),
            new Healer("Luna")
        };

        int round = 1;

        while (team1.Any(c => c.IsAlive) &&
               team2.Any(c => c.IsAlive))
        {
            Console.WriteLine($"\n========== Раунд {round} ==========");

            TeamTurn(team1, team2);

            if (!team2.Any(c => c.IsAlive))
                break;

            TeamTurn(team2, team1);

            round++;
        }

        Console.WriteLine("\n========== КІНЕЦЬ БОЮ ==========");

        if (team1.Any(c => c.IsAlive))
            Console.WriteLine("Перемогла команда 1!");
        else
            Console.WriteLine("Перемогла команда 2!");
    }

    static void TeamTurn(List<Character> attackers, List<Character> defenders)
    {
        Random rnd = new Random();

        foreach (var character in attackers.Where(c => c.IsAlive))
        {
            var aliveEnemies = defenders.Where(e => e.IsAlive).ToList();

            if (aliveEnemies.Count == 0)
                return;

            var target = aliveEnemies[rnd.Next(aliveEnemies.Count)];

            int action = rnd.Next(2);

            if (action == 0)
            {
                character.Attack(target);
            }
            else
            {
                character.SpecialAction(attackers, defenders);
            }
        }
    }
}






//Вам необхідно реалізувати простий логер, який записує дії користувача текстового файлу. При цьому важливо, щоб у всьому додатку
//використовувався єдиний екземпляр логера —, для цього застосуйте патерн Singleton.

//Вимоги:

//1) Клас Logger повинен реалізовувати патерн Singleton: приватний конструктор, статичне поле для зберігання єдиного екземпляра, публічний метод чи властивість доступу до екземпляра. 
//2) Метод Log(string message) записує надіслане повідомлення у файл log.txt (у директорії поруч із додатком), додаючи поточний час.
//3) Створіть тестовий клас Program, що викликає Logger.Instance.Log(...) з різних місць (наприклад, із різних методів).


class Logger
{
    private static Logger instance;

    private string filePath = "log.txt";

    private Logger()
    {
    }

    public static Logger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Logger();
            }

            return instance;
        }
    }

    public void Log(string message)
    {
        string logMessage =
            $"[{DateTime.Now}] {message}\n";

        File.AppendAllText(filePath, logMessage);
    }
}

class Program
{
    static void Main()
    {
        Logger.Instance.Log("Програма запущена");

        OpenMenu();

        SaveData();

        Logger.Instance.Log("Програма завершила роботу");

        Console.WriteLine("Логи записані у файл log.txt");
    }

    static void OpenMenu()
    {
        Logger.Instance.Log("Користувач відкрив меню");
    }

    static void SaveData()
    {
        Logger.Instance.Log("Користувач зберіг дані");
    }
}
