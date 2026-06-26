using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        using (var context = new AppDbContext())
        {
            if (!context.Students.Any())
            {
                var inst1 = new Instructor { FirstName = "Олександр", LastName = "Коваленко" };
                var inst2 = new Instructor { FirstName = "Ірина", LastName = "Ткаченко" };
                context.Instructors.AddRange(inst1, inst2);

                var course1 = new Course { Title = "Програмування C#", Description = "Базовий курс C# та .NET", Instructor = inst1 };
                var course2 = new Course { Title = "Бази даних SQL", Description = "Проєктування та запити", Instructor = inst1 };
                var course3 = new Course { Title = "Веб-розробка", Description = "Створення сайтів", Instructor = inst2 };
                var course4 = new Course { Title = "Розробка мобільних додатків", Description = "Курс по Flutter", Instructor = inst2 };
                context.Courses.AddRange(course1, course2, course3, course4);

                var st1 = new Student { FirstName = "Іван", LastName = "Петренко", DateOfBirth = new DateTime(2003, 5, 12) }; // 23 роки
                var st2 = new Student { FirstName = "Ганна", LastName = "Лисенко", DateOfBirth = new DateTime(1998, 10, 3) }; // 27 років (більше 25)
                var st3 = new Student { FirstName = "Максим", LastName = "Шевченко", DateOfBirth = new DateTime(2005, 1, 20) }; // 21 рік
                var st4 = new Student { FirstName = "Олена", LastName = "Бойко", DateOfBirth = new DateTime(1995, 3, 15) }; // 31 рік (більше 25)
                var st5 = new Student { FirstName = "Дмитро", LastName = "Козак", DateOfBirth = new DateTime(2002, 7, 8) }; // 23 роки
                var st6 = new Student { FirstName = "Марія", LastName = "Мороз", DateOfBirth = new DateTime(2004, 11, 30) }; // 21 рік
                context.Students.AddRange(st1, st2, st3, st4, st5, st6);

                context.Enrollments.AddRange(
                    new Enrollment { Student = st1, Course = course1, EnrollmentDate = DateTime.Now },
                    new Enrollment { Student = st1, Course = course2, EnrollmentDate = DateTime.Now },

                    new Enrollment { Student = st2, Course = course1, EnrollmentDate = DateTime.Now },
                    new Enrollment { Student = st2, Course = course3, EnrollmentDate = DateTime.Now },

                    new Enrollment { Student = st3, Course = course1, EnrollmentDate = DateTime.Now },

                    new Enrollment { Student = st4, Course = course1, EnrollmentDate = DateTime.Now },
                    new Enrollment { Student = st4, Course = course2, EnrollmentDate = DateTime.Now },
                    new Enrollment { Student = st4, Course = course3, EnrollmentDate = DateTime.Now },

                    new Enrollment { Student = st5, Course = course1, EnrollmentDate = DateTime.Now },

                    new Enrollment { Student = st6, Course = course1, EnrollmentDate = DateTime.Now }
                );

                context.SaveChanges();
                Console.WriteLine("Базу даних успішно наповнено тестовими даними!");
                Console.WriteLine("==================================================\n");
            }
        }

        using (var context = new AppDbContext())
        {
            int currentYear = DateTime.Now.Year;

            Console.WriteLine("--- ВИКОНАННЯ LINQ ЗАПИТІВ --- \n");

            int targetCourseId = 1;
            var q1 = context.Students
                .Where(s => s.Enrollments.Any(e => e.CourseId == targetCourseId))
                .ToList();
            Console.WriteLine($"1) Студенти на курсі з Id {targetCourseId}: " + string.Join(", ", q1.Select(s => $"{s.FirstName} {s.LastName}")));

            int targetInstructorId = 1;
            var q2 = context.Courses
                .Where(c => c.InstructorId == targetInstructorId)
                .ToList();
            Console.WriteLine($"2) Курси викладача з Id {targetInstructorId}: " + string.Join(", ", q2.Select(c => c.Title)));

            var q3 = context.Courses
                .Where(c => c.InstructorId == targetInstructorId)
                .Select(c => new
                {
                    CourseTitle = c.Title,
                    StudentNames = c.Enrollments.Select(e => e.Student.FirstName + " " + e.Student.LastName).ToList()
                })
                .ToList();
            Console.WriteLine($"3) Курси викладача {targetInstructorId} та їх студенти:");
            foreach (var item in q3)
            {
                Console.WriteLine($"   - {item.CourseTitle}: " + (item.StudentNames.Any() ? string.Join(", ", item.StudentNames) : "немає студентів"));
            }

            var q4 = context.Courses
                .Where(c => c.Enrollments.Count > 5)
                .ToList();
            Console.WriteLine($"4) Курси, де > 5 студентів: " + (q4.Any() ? string.Join(", ", q4.Select(c => c.Title)) : "таких курсів немає"));

            var q5 = context.Students
                .Where(s => (currentYear - s.DateOfBirth.Year) >= 25)
                .ToList();
            Console.WriteLine($"5) Студенти віком від 25 років: " + string.Join(", ", q5.Select(s => $"{s.FirstName} {s.LastName} ({currentYear - s.DateOfBirth.Year} р.)")));

            double avgAge = context.Students
                .Select(s => currentYear - s.DateOfBirth.Year)
                .Average();
            Console.WriteLine($"6) Середній вік усіх студентів: {avgAge:F1} років");

            var q7 = context.Students
                .OrderByDescending(s => s.DateOfBirth)
                .FirstOrDefault();
            Console.WriteLine($"7) Наймолодший студент: {q7?.FirstName} {q7?.LastName} ({q7?.DateOfBirth.ToShortDateString()})");

            int targetStudentId = 1;
            int courseCount = context.Enrollments
                .Count(e => e.StudentId == targetStudentId);
            Console.WriteLine($"8) Кількість курсів у студента з Id {targetStudentId}: {courseCount}");

            var q9 = context.Students
                .Select(s => s.FirstName)
                .ToList();
            Console.WriteLine($"9) Список імен усіх студентів: " + string.Join(", ", q9));

            var q10 = context.Students
                .AsEnumerable() 
                .GroupBy(s => currentYear - s.DateOfBirth.Year)
                .ToList();
            Console.WriteLine($"10) Групування студентів за віком:");
            foreach (var group in q10)
            {
                Console.WriteLine($"    Вік {group.Key} р.: " + string.Join(", ", group.Select(s => s.FirstName)));
            }

            var q11 = context.Students
                .OrderBy(s => s.LastName)
                .ToList();
            Console.WriteLine($"11) Студенти за алфавітом (прізвище): " + string.Join(", ", q11.Select(s => s.LastName + " " + s.FirstName)));

            var q12 = context.Students
                .Select(s => new
                {
                    StudentName = s.FirstName + " " + s.LastName,
                    Courses = s.Enrollments.Select(e => e.Course.Title).ToList()
                })
                .ToList();
            Console.WriteLine($"12) Студенти та їх курси:");
            foreach (var item in q12)
            {
                Console.WriteLine($"    - {item.StudentName} навчається на: " + (item.Courses.Any() ? string.Join(", ", item.Courses) : "жодному курсі"));
            }

            int nonTargetCourseId = 2;
            var q13 = context.Students
                .Where(s => !s.Enrollments.Any(e => e.CourseId == nonTargetCourseId))
                .ToList();
            Console.WriteLine($"13) Студенти, які НЕ записані на курс з Id {nonTargetCourseId}: " + string.Join(", ", q13.Select(s => s.FirstName + " " + s.LastName)));

            int courseA = 1;
            int courseB = 2;
            var q14 = context.Students
                .Where(s => s.Enrollments.Any(e => e.CourseId == courseA) && s.Enrollments.Any(e => e.CourseId == courseB))
                .ToList();
            Console.WriteLine($"14) Студенти, які одночасно на курсах {courseA} та {courseB}: " + string.Join(", ", q14.Select(s => s.FirstName + " " + s.LastName)));

            var q15 = context.Courses
                .Select(c => new
                {
                    CourseTitle = c.Title,
                    Count = c.Enrollments.Count
                })
                .ToList();
            Console.WriteLine($"15) Кількість студентів на кожному курсі:");
            foreach (var item in q15)
            {
                Console.WriteLine($"    - {item.CourseTitle}: {item.Count} студ.");
            }
        }

        Console.ReadLine();
    }
}