using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Необхідно розробити клас черги друку документів. 
//Передбачити у класі методи: 
//1) Приміщення документа у чергу друку.
//2) Вилучення наступного документа з черги друку.
//3) Перевірка наявності документів у черзі.
//При приміщенні документа задається його пріоритет, витягуються насамперед документи з вищим пріоритетом. Для пріоритету використовуйте Enum.

namespace PrintQueueExample
{
    enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    class Document
    {
        public string Name { get; set; }
        public Priority Priority { get; set; }

        public Document(string name, Priority priority)
        {
            Name = name;
            Priority = priority;
        }
    }

    class PrintQueue
    {
        private List<Document> documents = new List<Document>();

        public void AddDocument(Document doc)
        {
            documents.Add(doc);

            Console.WriteLine(
                $"Документ \"{doc.Name}\" додано " +
                $"з пріоритетом {doc.Priority}"
            );
        }

        public Document GetNextDocument()
        {
            if (!HasDocuments())
            {
                Console.WriteLine("Черга порожня!");
                return null;
            }

            Document highest = documents[0];

            foreach (Document doc in documents)
            {
                if (doc.Priority > highest.Priority)
                {
                    highest = doc;
                }
            }

            documents.Remove(highest);

            return highest;
        }

        public bool HasDocuments()
        {
            return documents.Count > 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PrintQueue queue = new PrintQueue();

            queue.AddDocument(new Document("Звіт.docx", Priority.Medium));
            queue.AddDocument(new Document("Контракт.pdf", Priority.High));
            queue.AddDocument(new Document("Фото.jpg", Priority.Low));

            Console.WriteLine();

            while (queue.HasDocuments())
            {
                Document doc = queue.GetNextDocument();

                Console.WriteLine(
                    $"Друкується документ: {doc.Name} " +
                    $"(Пріоритет: {doc.Priority})"
                );
            }

            Console.ReadKey();
        }
    }
}

//Створити клас «Аеропорт». Реалізувати додавання черги пасажирів літаком. Реалізувати посадку на літак за допомогою Queue.
//    У консолі виводити хтось зареєструвався, хтось сів на літак.

class Passenger
{
    public string Name { get; set; }

    public Passenger(string name)
    {
        Name = name;
    }
}

class Airport
{
    private Queue<Passenger> passengersQueue = new Queue<Passenger>();

    public void RegisterPassenger(Passenger passenger)
    {
        passengersQueue.Enqueue(passenger);
        Console.WriteLine($"{passenger.Name} зареєструвався на рейс.");
    }

    public void Boarding()
    {
        Console.WriteLine("\n--- Початок посадки на літак ---\n");

        while (passengersQueue.Count > 0)
        {
            Passenger p = passengersQueue.Dequeue();
            Console.WriteLine($"{p.Name} сів на літак.");
        }

        Console.WriteLine("\nУсі пасажири на борту.");
    }

    public bool HasPassengers()
    {
        return passengersQueue.Count > 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Airport airport = new Airport();

        airport.RegisterPassenger(new Passenger("Іван"));
        airport.RegisterPassenger(new Passenger("Олена"));
        airport.RegisterPassenger(new Passenger("Марія"));
        airport.RegisterPassenger(new Passenger("Петро"));

        if (airport.HasPassengers())
        {
            airport.Boarding();
        }
    }
}