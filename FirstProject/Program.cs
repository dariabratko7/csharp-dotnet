using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Завдання 1
//Користувач вводить із клавіатури число в діапазоні від 1 до 100. Якщо число кратне 3 (ділиться на 3 без залишку) 
//потрібно вивести слово Fizz. Якщо число кратне 5 потрібно вивести слово Buzz. Якщо число кратне 3 і 5 потрібно вивести Fizz Buzz.
//Якщо число не кратне не 3 і 5 потрібно вивести саме число.
//Якщо користувач ввів значення не в діапазоні від 1 до 100 потрібно вивести повідомлення про помилку.
//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("ВВедіть число від 1 до 100: ");

//            if (!int.TryParse(Console.ReadLine(), out int number))
//            {
//                Console.WriteLine("Це не число!");
//                return;
//            }
//            if (number < 1 || number > 100)
//            {
//                Console.WriteLine("Помилка! Число повинно бути від 1 до 100.");
//            }
//            else if (number % 3 == 0 && number % 5 == 0)
//            {
//                Console.WriteLine("Fizz Buzz");
//            }
//            else if (number % 3 == 0)
//            {
//                Console.WriteLine("Fizz");
//            }
//            else if (number % 5 == 0)
//            {
//                Console.WriteLine("Buzz");
//            }
//            else
//            {
//                Console.WriteLine("number");
//            }
//        }
//    }
//}

//Завдання 2
//Користувач вводить із клавіатури два числа. Перше число — це значення, друге число відсоток, який необхідно порахувати. 
//Наприклад, ми ввели з клавіатури 90 і 10. Потрібно вивести на екран 10 відсотків від 90. Результат: 9.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть число: ");
//            if (!double.TryParse(Console.ReadLine(), out double number))
//            {
//                Console.WriteLine("Помилка! Це не число.");
//                return;
//            }

//            Console.Write("Введіть відсоток: ");
//            if (!double.TryParse(Console.ReadLine(), out double percent))
//            {
//                Console.WriteLine("Помилка! Це не число.");
//                return;
//            }

//            double result = number * percent / 100;

//            Console.WriteLine($"{percent}% від {number} = {result}");
//        }
//    }
//}

//Завдання 3
//Користувач вводить із клавіатури чотири цифри. Необхідно створити число, що містить ці цифри. 
//Наприклад, якщо з клавіатури введено 1, 5, 7, 8, тоді потрібно сформувати число 1578.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть першу цифру: ");
//            int a = int.Parse(Console.ReadLine());

//            Console.Write("Введіть другу цифру: ");
//            int b = int.Parse(Console.ReadLine());

//            Console.Write("Введіть третю цифру: ");
//            int c = int.Parse(Console.ReadLine());

//            Console.Write("Введіть четверту цифру: ");
//            int d = int.Parse(Console.ReadLine());

//            int result = a * 1000 + b * 100 + c * 10 + d;

//            Console.WriteLine($"Сформоване число: {result}");
//        }
//    }
//}

//Завдання 4
//Користувач вводить шестизначне число. Після чого користувач вводить номери розрядів для обміну цифр.
//    Наприклад, якщо користувач ввів один і шість - це означає, що треба обміняти місцями першу і шосту цифри.
//Число 723895 має перетворитися на 523897.
//Якщо користувач ввів не шестизначне число, потрібно вивести повідомлення про помилку.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть шестизначне число: ");
//            string number = Console.ReadLine();

//            if (number.Length != 6 || !int.TryParse(number, out _))
//            {
//                Console.WriteLine("Помилка! Потрібно ввести шестизначне число.");
//                return;
//            }

//            Console.Write("Введіть перший розряд (1-6): ");
//            int pos1 = int.Parse(Console.ReadLine());

//            Console.Write("Введіть другий розряд (1-6): ");
//            int pos2 = int.Parse(Console.ReadLine());

//            if (pos1 < 1 || pos1 > 6 || pos2 < 1 || pos2 > 6)
//            {
//                Console.WriteLine("Помилка! Розряди мають бути від 1 до 6.");
//                return;
//            }

//            char[] digits = number.ToCharArray();

//            char temp = digits[pos1 - 1];
//            digits[pos1 - 1] = digits[pos2 - 1];
//            digits[pos2 - 1] = temp;

//            string result = new string(digits);

//            Console.WriteLine($"Результат: {result}");
//        }
//    }
//}

//Завдання 5
//Користувач вводить із клавіатури дату. Додаток має відобразити назву сезону і дня тижня. 
//    Наприклад, якщо введено 22.12.2021, додаток має відобразити Winter Wednesday.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть дату (наприклад 22.12.2021): ");

//            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
//            {
//                Console.WriteLine("Помилка! Невірний формат дати.");
//                return;
//            }

//            string day = date.DayOfWeek.ToString();

//            int month = date.Month;

//            string season;

//            if (month == 12 || month == 1 || month == 2)
//                season = "Winter";
//            else if (month >= 3 && month <= 5)
//                season = "Spring";
//            else if (month >= 6 && month <= 8)
//                season = "Summer";
//            else
//                season = "Autumn";

//            Console.WriteLine($"{season} {day}");
//        }
//    }
//}

//Завдання 6
//Користувач вводить із клавіатури показання температури.
//    Залежно від вибору користувача програма переводить температуру з Фаренгейта в Цельсій або навпаки.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть температуру: ");
//            if (!double.TryParse(Console.ReadLine(), out double temp))
//            {
//                Console.WriteLine("Помилка! Це не число.");
//                return;
//            }

//            Console.WriteLine("Оберіть напрям переводу:");
//            Console.WriteLine("1 - Фаренгейт → Цельсій");
//            Console.WriteLine("2 - Цельсій → Фаренгейт");

//            int choice = int.Parse(Console.ReadLine());

//            if (choice == 1)
//            {
//                double result = (temp - 32) * 5 / 9;
//                Console.WriteLine($"Результат: {result} °C");
//            }
//            else if (choice == 2)
//            {
//                double result = temp * 9 / 5 + 32;
//                Console.WriteLine($"Результат: {result} °F");
//            }
//            else
//            {
//                Console.WriteLine("Невірний вибір!");
//            }
//        }
//    }
//}

//Завдання 7
//Користувач вводить з клавіатури два числа. Потрібно показати всі парні числа у вказаному діапазоні.
//    Якщо межі діапазону вказані неправильно, потрібно провести нормалізацію меж. 
//    Наприклад, користувач ввів 20 і 11, потрібна нормалізація, після якої початок діапазону стане дорівнювати 11, а кінець 20.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть перше число: ");
//            if (!int.TryParse(Console.ReadLine(), out int a))
//            {
//                Console.WriteLine("Помилка!");
//                return;
//            }

//            Console.Write("Введіть друге число: ");
//            if (!int.TryParse(Console.ReadLine(), out int b))
//            {
//                Console.WriteLine("Помилка!");
//                return;
//            }

//            if (a > b)
//            {
//                int temp = a;
//                a = b;
//                b = temp;
//            }

//            Console.WriteLine("Парні числа:");

//            for (int i = a; i <= b; i++)
//            {
//                if (i % 2 == 0)
//                {
//                    Console.Write(i + " ");
//                }
//            }
//        }
//    }
//}

//Завдання 8
//Користувач вводить число. Програма повинна визначити, чи є це число числом Армстронга 
//(число Армстронга — це таке число, що дорівнює сумі своїх цифр, піднесених до степеня, що дорівнює кількості цих цифр). 
//Наприклад, число 153 є числом Армстронга, оскільки 1^3 + 5^3 + 3^3 = 153.

//namespace FirstProject
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.Write("Введіть число: ");

//            if (!int.TryParse(Console.ReadLine(), out int number))
//            {
//                Console.WriteLine("Помилка!");
//                return;
//            }

//            int original = number;
//            int length = number.ToString().Length;
//            int sum = 0;

//            while (number > 0)
//            {
//                int digit = number % 10; // остання цифра
//                sum += (int)Math.Pow(digit, length);
//                number /= 10; // відрізаємо цифру
//            }

//            if (sum == original)
//            {
//                Console.WriteLine("Це число Армстронга");
//            }
//            else
//            {
//                Console.WriteLine("Це НЕ число Армстронга");
//            }
//        }
//    }
//}

//Завдання 9
//Користувач вводить число. Програма повинна визначити, чи є це число досконалим.
//Досконале число — це число, яке дорівнює сумі всіх своїх дільників, крім самого себе (наприклад, 28 = 1 + 2 + 4 + 7 + 14).


namespace FirstProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введіть число: ");
        int number = int.Parse(Console.ReadLine());

        int sum = 0;

        for (int i = 1; i <= number / 2; i++)
        {
            if (number % i == 0)
            {
                sum += i;
            }
        }

        if (sum == number && number != 0)
        {
            Console.WriteLine($"{number} є досконалим числом.");
        }
        else
        {
            Console.WriteLine($"{number} не є досконалим числом.");
        }
    }
}
