using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz3003
{
    internal class Program
    {
//Описати клас «Домашня бібліотека».
//            Передбачити можливість роботи з довільною кількістю книг, 
//            пошуку книги за якоюсь ознакою(наприклад, за автором або за роком видання),
//            додавання книг до бібліотеки, видалення книг з неї, сортування книг з різних полів.

class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Title} | {Author} | {Year}";
        }
    }

    class HomeLibrary
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void RemoveBook(string title)
        {
            books.RemoveAll(b => b.Title == title);
        }

        public List<Book> FindByAuthor(string author)
        {
            return books.Where(b => b.Author == author).ToList();
        }

        public List<Book> FindByYear(int year)
        {
            return books.Where(b => b.Year == year).ToList();
        }

        public void SortByTitle()
        {
            books = books.OrderBy(b => b.Title).ToList();
        }

        public void SortByAuthor()
        {
            books = books.OrderBy(b => b.Author).ToList();
        }

        public void SortByYear()
        {
            books = books.OrderBy(b => b.Year).ToList();
        }

        public void PrintAll()
        {
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
    }

    class Progra
    {
        static void Main()
        {
            HomeLibrary library = new HomeLibrary();

            library.AddBook(new Book("Кобзар", "Шевченко", 1840));
            library.AddBook(new Book("1984", "Оруелл", 1949));
            library.AddBook(new Book("Майстер і Маргарита", "Булгаков", 1967));

            Console.WriteLine("Всі книги:");
            library.PrintAll();

            Console.WriteLine("\nКниги Шевченка:");
            var found = library.FindByAuthor("Шевченко");
            foreach (var book in found)
                Console.WriteLine(book);

            library.SortByYear();
            Console.WriteLine("\nПісля сортування за роком:");
            library.PrintAll();

            library.RemoveBook("1984");
            Console.WriteLine("\nПісля видалення:");
            library.PrintAll();
        }
    }
}
}
//Описати клас User з полями: Id, Login, PasswordHash, Email.
//    У класі реалізувати 2 методи сортування: за Email-адресами та Login.
//    Створити масив користувачів на 5 елементів, вивести їх у відсортованому порядку.
//    Методи зробити статичними. ПРИМІТКА: Після виклику методів сортування початковий масив повинен мати відсортовані значення в собі.
class User
{
    public int Id;
    public string Login;
    public string PasswordHash;
    public string Email;

    public User(int id, string login, string passwordHash, string email)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
        Email = email;
    }

    public static void SortByEmail(User[] users)
    {
        Array.Sort(users, (a, b) => string.Compare(a.Email, b.Email));
    }

    public static void SortByLogin(User[] users)
    {
        Array.Sort(users, (a, b) => string.Compare(a.Login, b.Login));
    }

    public void Print()
    {
        Console.WriteLine($"{Id} | {Login} | {Email}");
    }
}

class Program
{
    static void Main()
    {
        User[] users = new User[5]
        {
            new User(1, "ivan", "hash1", "ivan@gmail.com"),
            new User(2, "anna", "hash2", "anna@yahoo.com"),
            new User(3, "petro", "hash3", "petro@gmail.com"),
            new User(4, "olga", "hash4", "olga@ukr.net"),
            new User(5, "andriy", "hash5", "andriy@gmail.com")
        };

        Console.WriteLine("До сортування:");
        PrintArray(users);

        User.SortByEmail(users);
        Console.WriteLine("\nПісля сортування за Email:");
        PrintArray(users);

        User.SortByLogin(users);
        Console.WriteLine("\nПісля сортування за Login:");
        PrintArray(users);
    }

    static void PrintArray(User[] users)
    {
        foreach (var user in users)
        {
            user.Print();
        }
    }
}