using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

//1 завдання

namespace EFCoreApp
{

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public int Year { get; set; }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime Date { get; set; }
    }


    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("AppDb");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();


            db.Movies.AddRange(
                new Movie { Title = "Film1", Rating = 4.5, Year = 2001 },
                new Movie { Title = "Film2", Rating = 3.2, Year = 2010 },
                new Movie { Title = "Film3", Rating = 4.0, Year = 2005 }
            );
            db.SaveChanges();

            var filteredMovies = db.Movies
                .Where(m => m.Rating >= 3.5 && m.Rating <= 5)
                .ToList();

            Console.WriteLine("Movies in rating range:");
            foreach (var m in filteredMovies)
                Console.WriteLine($"{m.Title} - {m.Rating}");

            var moviesToUpdate = db.Movies.Take(3).ToList();
            foreach (var m in moviesToUpdate)
                m.Rating += 0.5;

            db.SaveChanges();

            var badMovies = db.Movies
                .Where(m => m.Rating < 3.4)
                .ToList();

            db.Movies.RemoveRange(badMovies);
            db.SaveChanges();

            Movie detachedMovie;

            using (var temp = new AppDbContext())
            {
                detachedMovie = temp.Movies.FirstOrDefault();
            }

            if (detachedMovie != null)
            {
                detachedMovie.Rating = 9.9;

                using (var temp = new AppDbContext())
                {
                    temp.Movies.Update(detachedMovie);
                    temp.SaveChanges();
                }
            }
 
            var oldMovies = db.Movies
                .Where(m => m.Year < 2005)
                .ToList();

            foreach (var m in oldMovies)
                m.Rating += 0.1;

            db.SaveChanges();
 
            db.Tasks.Add(new TaskItem
            {
                Title = "Do homework",
                IsDone = false,
                Date = DateTime.Today
            });

            db.SaveChanges();
 
            var tasks = db.Tasks.ToList();

            Console.WriteLine("\nTasks:");
            foreach (var t in tasks)
                Console.WriteLine($"{t.Title} - {t.IsDone}");
 
            var taskToComplete = db.Tasks.FirstOrDefault();
            if (taskToComplete != null)
            {
                taskToComplete.IsDone = true;
                db.SaveChanges();
            }
 
            var todayTasks = db.Tasks
                .Where(t => t.Date == DateTime.Today)
                .ToList();

            Console.WriteLine("\nToday tasks:");
            foreach (var t in todayTasks)
                Console.WriteLine(t.Title);
 
            var doneTasks = db.Tasks
                .Where(t => t.IsDone)
                .ToList();

            db.Tasks.RemoveRange(doneTasks);
            db.SaveChanges();
        }
    }
}


//2 завдання
public class MenuDish
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Category { get; set; } = "";
}

public class AppDbContext : DbContext
{
    public DbSet<MenuDish> MenuDishes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

class Program
{
    static void Main(string[] args)
    {
        using (var db = new AppDbContext())
        { 
            if (!db.Database.CanConnect())
            {
                Console.WriteLine("База даних недоступна");
                return;
            }
 
            var dish = new MenuDish
            {
                Name = "Суп грибний",
                Price = 120,
                Category = "Суп"
            };

            db.MenuDishes.Add(dish);
 
            var dishes = new List<MenuDish>
                {
                    new MenuDish { Name = "Суп курячий", Price = 110, Category = "Суп" },
                    new MenuDish { Name = "Паста Карбонара", Price = 180, Category = "Гаряче" },
                    new MenuDish { Name = "Суп овочевий", Price = 100, Category = "Суп" }
                };

            db.MenuDishes.AddRange(dishes);

            db.SaveChanges();
 
            var soups = db.MenuDishes
                .Where(d => d.Name.Contains("Суп"))
                .ToList();

            Console.WriteLine("Супи:");
            foreach (var s in soups)
            {
                Console.WriteLine($"{s.Id} - {s.Name}");
            }
 
            int id = 1;
            var dishById = db.MenuDishes.FirstOrDefault(d => d.Id == id);

            Console.WriteLine("\nСтрава за ID:");
            if (dishById != null)
                Console.WriteLine(dishById.Name);
 
            var lastDish = db.MenuDishes
                .OrderByDescending(d => d.Id)
                .FirstOrDefault();

            Console.WriteLine("\nОстання страва:");
            if (lastDish != null)
                Console.WriteLine(lastDish.Name);
        }

        Console.WriteLine("\nГотово!");
    }
}
