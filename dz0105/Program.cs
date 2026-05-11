using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Створіть часткову структуру з ім'ям «Student», що містить поля: прізвище та ініціали, номер групи, успішність (масив із п'яти елементів). 
//Створіть масив із десяти елементів такого типу. Впорядкувати записи щодо зростання середнього балу. Використовувати сортування IComparable і спосіб розширення.

namespace StudentSort
{
    public partial struct Student : IComparable<Student>
    {
        public string FullName;
        public string Group;
        public int[] Grades;

        public double AverageGrade()
        {
            return Grades.Average();
        }

        public int CompareTo(Student other)
        {
            return this.AverageGrade().CompareTo(other.AverageGrade());
        }
    }


    public partial struct Student
    {
        public void ShowInfo()
        {
            Console.WriteLine(
                $"Студент: {FullName}, " +
                $"Група: {Group}, " +
                $"Середній бал: {AverageGrade():F2}"
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student[] students = new Student[]
            {
                new Student
                {
                    FullName = "Іваненко І.І.",
                    Group = "KN-21",
                    Grades = new int[] { 5, 4, 5, 4, 5 }
                },

                new Student
                {
                    FullName = "Петренко П.П.",
                    Group = "KN-22",
                    Grades = new int[] { 3, 4, 3, 4, 3 }
                },

                new Student
                {
                    FullName = "Сидоренко С.С.",
                    Group = "KN-23",
                    Grades = new int[] { 5, 5, 5, 5, 5 }
                },

                new Student
                {
                    FullName = "Коваленко К.К.",
                    Group = "KN-24",
                    Grades = new int[] { 4, 4, 4, 4, 4 }
                },

                new Student
                {
                    FullName = "Мельник М.М.",
                    Group = "KN-25",
                    Grades = new int[] { 2, 3, 2, 3, 2 }
                },

                new Student
                {
                    FullName = "Шевченко Ш.Ш.",
                    Group = "KN-26",
                    Grades = new int[] { 5, 4, 4, 5, 4 }
                },

                new Student
                {
                    FullName = "Ткаченко Т.Т.",
                    Group = "KN-27",
                    Grades = new int[] { 3, 3, 4, 3, 4 }
                },

                new Student
                {
                    FullName = "Бондар Б.Б.",
                    Group = "KN-28",
                    Grades = new int[] { 5, 5, 4, 5, 5 }
                },

                new Student
                {
                    FullName = "Олійник О.О.",
                    Group = "KN-29",
                    Grades = new int[] { 4, 3, 4, 3, 4 }
                },

                new Student
                {
                    FullName = "Гриценко Г.Г.",
                    Group = "KN-30",
                    Grades = new int[] { 5, 3, 5, 3, 5 }
                }
            };

            Console.WriteLine("До сортування:\n");

            foreach (Student student in students)
            {
                student.ShowInfo();
            }

            Array.Sort(students);

            Console.WriteLine("\nПісля сортування за середнім балом:\n");

            foreach (Student student in students)
            {
                student.ShowInfo();
            }

            Console.ReadKey();
        }
    }
}

//Сформувати клас на 5 властивостей. Створити масив об'єктів класу відобразити консолі. 
//    Використовуючи метод розширення «Select» і анонімний тип, отримати масив лише з 2-гом'я властивостями класу.

namespace SelectExample
{
    class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees =
            {
                new Employee
                {
                    Name = "Іван",
                    Age = 25,
                    Position = "Програміст",
                    Salary = 35000,
                    City = "Київ"
                },

                new Employee
                {
                    Name = "Марія",
                    Age = 30,
                    Position = "Дизайнер",
                    Salary = 28000,
                    City = "Львів"
                },

                new Employee
                {
                    Name = "Олег",
                    Age = 27,
                    Position = "Тестувальник",
                    Salary = 25000,
                    City = "Одеса"
                }
            };

            Console.WriteLine("Повна інформація:\n");

            foreach (Employee emp in employees)
            {
                Console.WriteLine(
                    $"Ім'я: {emp.Name}, " +
                    $"Вік: {emp.Age}, " +
                    $"Посада: {emp.Position}, " +
                    $"Зарплата: {emp.Salary}, " +
                    $"Місто: {emp.City}"
                );
            }

            var shortInfo = employees.Select(emp => new
            {
                emp.Name,
                emp.Position
            });

            Console.WriteLine("\nЛише 2 властивості:\n");

            foreach (var emp in shortInfo)
            {
                Console.WriteLine(
                    $"Ім'я: {emp.Name}, " +
                    $"Посада: {emp.Position}"
                );
            }

            Console.ReadKey();
        }
    }
}