using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Lesson2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //    Завдання 1
            //Оголосити одновимірний(5 елементів) масив з іменем A і двовимірний масив(3 рядки, 4 стовпці) дробових чисел з іменем B.
            //Заповнити одновимірний масив А числами, введеними з клавіатури користувачем, 
            //а двовимірний масив В випадковими числами за допомогою циклів.Вивести на екран 
            //значення масивів: масиву А в один рядок, масиву В — у вигляді матриці.
            //Знайти в даних масивах загальний максимальний елемент, мінімальний елемент,
            //загальну суму всіх елементів, загальний добуток усіх елементів, суму парних елементів масиву А,
            //суму непарних стовпців масиву В.
            //int[] A = new int[5];
            //double[,] B = new double[3, 4];
            //Random rand = new Random();


            //Console.WriteLine("Введіть 5 елементів масиву A:");
            //for (int i = 0; i < A.Length; i++)
            //{
            //    Console.Write($"A[{i}] = ");
            //    A[i] = int.Parse(Console.ReadLine());
            //}


            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        B[i, j] = rand.NextDouble() * 100;
            //    }
            //}


            //Console.WriteLine("\nМасив A:");
            //foreach (int x in A)
            //{
            //    Console.Write(x + " ");
            //}


            //Console.WriteLine("\n\nМасив B:");
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Console.Write($"{B[i, j]:F2}\t");
            //    }
            //    Console.WriteLine();
            //}


            //double max = A[0];
            //double min = A[0];
            //double sum = 0;
            //double product = 1;


            //foreach (int x in A)
            //{
            //    if (x > max) max = x;
            //    if (x < min) min = x;
            //    sum += x;
            //    product *= x;
            //}


            //foreach (double x in B)
            //{
            //    if (x > max) max = x;
            //    if (x < min) min = x;
            //    sum += x;
            //    product *= x;
            //}


            //int sumEvenA = 0;
            //foreach (int x in A)
            //{
            //    if (x % 2 == 0)
            //        sumEvenA += x;
            //}


            //double sumOddColumnsB = 0;
            //for (int j = 0; j < 4; j++)
            //{
            //    if (j % 2 != 0)
            //    {
            //        for (int i = 0; i < 3; i++)
            //        {
            //            sumOddColumnsB += B[i, j];
            //        }
            //    }
            //}


            //Console.WriteLine("\n\nРезультати:");
            //Console.WriteLine($"Максимум: {max}");
            //Console.WriteLine($"Мінімум: {min}");
            //Console.WriteLine($"Сума: {sum}");
            //Console.WriteLine($"Добуток: {product}");
            //Console.WriteLine($"Сума парних елементів A: {sumEvenA}");
            //Console.WriteLine($"Сума непарних стовпців B: {sumOddColumnsB:F2}");



            //            Завдання 2
            //Дано двовимірний масив розмірністю 5×5, заповнений випадковими числами з діапазону 
            //                від -100 до 100.Визначити суму елементів масиву, розташованих між мінімальним і максимальним елементами.

            //int[,] arr = new int[5, 5];
            //Random rand = new Random();


            //Console.WriteLine("Масив:");
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        arr[i, j] = rand.Next(-100, 101);
            //        Console.Write($"{arr[i, j],5}");
            //    }
            //    Console.WriteLine();
            //}


            //int min = arr[0, 0], max = arr[0, 0];
            //int minIndex = 0, maxIndex = 0;

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        int index = i * 5 + j;

            //        if (arr[i, j] < min)
            //        {
            //            min = arr[i, j];
            //            minIndex = index;
            //        }

            //        if (arr[i, j] > max)
            //        {
            //            max = arr[i, j];
            //            maxIndex = index;
            //        }
            //    }
            //}


            //int start = Math.Min(minIndex, maxIndex);
            //int end = Math.Max(minIndex, maxIndex);


            //int sum = 0;

            //for (int k = start + 1; k < end; k++)
            //{
            //    int i = k / 5;
            //    int j = k % 5;
            //    sum += arr[i, j];
            //}


            //Console.WriteLine($"\nМінімум: {min}");
            //Console.WriteLine($"Максимум: {max}");
            //Console.WriteLine($"Сума між ними: {sum}");


            //            Завдання 3
            //Користувач вводить рядок із клавіатури.Необхідно зашифрувати цей рядок, використовуючи шифр Цезаря.
            //                З Вікіпедії: "Шифр Цезаря - це різновид шифру підстановки, в якому кожен символ у відкритому" +
            //                " тексті замінюється символом, що знаходиться на деякому постійному числі позицій лівіше" +
            //                " або правіше за нього в алфавіті. Наприклад, у шифрі зі зсувом вправо на 3, A була " +
            //                "б замінена на D, B стане E, і так далі".Докладніше тут.
            //Крім механізму шифрування, реалізуйте механізм розшифрування.


            //    Console.Write("Введіть рядок: ");
            //    string text = Console.ReadLine();

            //    Console.Write("Введіть зсув (ключ): ");
            //    int shift = int.Parse(Console.ReadLine());

            //    string encrypted = Encrypt(text, shift);
            //    string decrypted = Decrypt(encrypted, shift);

            //    Console.WriteLine($"\nЗашифрований текст: {encrypted}");
            //    Console.WriteLine($"Розшифрований текст: {decrypted}");
            //}

            //static string Encrypt(string text, int shift)
            //{
            //    char[] result = new char[text.Length];

            //    for (int i = 0; i < text.Length; i++)
            //    {
            //        char c = text[i];

            //        if (char.IsLetter(c))
            //        {
            //            char offset = char.IsUpper(c) ? 'A' : 'a';
            //            result[i] = (char)((c - offset + shift) % 26 + offset);
            //        }
            //        else
            //        {
            //            result[i] = c;
            //        }
            //    }

            //    return new string(result);
            //}

            //static string Decrypt(string text, int shift)
            //{
            //    return Encrypt(text, 26 - shift % 26);

            //            Завдання 4
            //Створіть додаток, який виконує операції над матрицями:
            //Множення матриці на число;
            //            Додавання матриць;
            //            Добуток матриць.
            Console.WriteLine("Оберіть операцію:");
            Console.WriteLine("1 - Множення матриці на число");
            Console.WriteLine("2 - Додавання матриць");
            Console.WriteLine("3 - Добуток матриць");

            //    int choice = int.Parse(Console.ReadLine());

            //    switch (choice)
            //    {
            //        case 1:
            //            MultiplyByNumber();
            //            break;
            //        case 2:
            //            AddMatrices();
            //            break;
            //        case 3:
            //            MultiplyMatrices();
            //            break;
            //        default:
            //            Console.WriteLine("Невірний вибір!");
            //            break;
            //    }
            //}

            //static int[,] InputMatrix(int rows, int cols)
            //{
            //    int[,] matrix = new int[rows, cols];
            //    Console.WriteLine($"Введіть елементи матриці {rows}x{cols}:");

            //    for (int i = 0; i < rows; i++)
            //    {
            //        for (int j = 0; j < cols; j++)
            //        {
            //            Console.Write($"[{i},{j}] = ");
            //            matrix[i, j] = int.Parse(Console.ReadLine());
            //        }
            //    }

            //    return matrix;
            //}


            //static void PrintMatrix(int[,] matrix)
            //{
            //    int rows = matrix.GetLength(0);
            //    int cols = matrix.GetLength(1);

            //    Console.WriteLine("Результат:");
            //    for (int i = 0; i < rows; i++)
            //    {
            //        for (int j = 0; j < cols; j++)
            //        {
            //            Console.Write($"{matrix[i, j],5}");
            //        }
            //        Console.WriteLine();
            //    }
            //}


            //static void MultiplyByNumber()
            //{
            //    Console.Write("Кількість рядків: ");
            //    int rows = int.Parse(Console.ReadLine());

            //    Console.Write("Кількість стовпців: ");
            //    int cols = int.Parse(Console.ReadLine());

            //    int[,] matrix = InputMatrix(rows, cols);

            //    Console.Write("Введіть число: ");
            //    int num = int.Parse(Console.ReadLine());

            //    int[,] result = new int[rows, cols];

            //    for (int i = 0; i < rows; i++)
            //        for (int j = 0; j < cols; j++)
            //            result[i, j] = matrix[i, j] * num;

            //    PrintMatrix(result);
            //}


            //static void AddMatrices()
            //{
            //    Console.Write("Кількість рядків: ");
            //    int rows = int.Parse(Console.ReadLine());

            //    Console.Write("Кількість стовпців: ");
            //    int cols = int.Parse(Console.ReadLine());

            //    Console.WriteLine("Перша матриця:");
            //    int[,] A = InputMatrix(rows, cols);

            //    Console.WriteLine("Друга матриця:");
            //    int[,] B = InputMatrix(rows, cols);

            //    int[,] result = new int[rows, cols];

            //    for (int i = 0; i < rows; i++)
            //        for (int j = 0; j < cols; j++)
            //            result[i, j] = A[i, j] + B[i, j];

            //    PrintMatrix(result);
            //}


            //static void MultiplyMatrices()
            //{
            //    Console.Write("Рядки першої матриці: ");
            //    int r1 = int.Parse(Console.ReadLine());

            //    Console.Write("Стовпці першої матриці: ");
            //    int c1 = int.Parse(Console.ReadLine());

            //    Console.Write("Рядки другої матриці: ");
            //    int r2 = int.Parse(Console.ReadLine());

            //    Console.Write("Стовпці другої матриці: ");
            //    int c2 = int.Parse(Console.ReadLine());

            //    if (c1 != r2)
            //    {
            //        Console.WriteLine("Множення неможливе!");
            //        return;
            //    }

            //    Console.WriteLine("Перша матриця:");
            //    int[,] A = InputMatrix(r1, c1);

            //    Console.WriteLine("Друга матриця:");
            //    int[,] B = InputMatrix(r2, c2);

            //    int[,] result = new int[r1, c2];

            //    for (int i = 0; i < r1; i++)
            //    {
            //        for (int j = 0; j < c2; j++)
            //        {
            //            for (int k = 0; k < c1; k++)
            //            {
            //                result[i, j] += A[i, k] * B[k, j];
            //            }
            //        }
            //    }

            //    PrintMatrix(result);
            //            Завдання 5
            //Користувач з клавіатури вводить у рядок арифметичний вираз. Додаток має порахувати його результат.
            //                Необхідно підтримувати тільки дві операції: +і -.

            //    Console.Write("Введіть вираз (тільки + і -): ");
            //    string input = Console.ReadLine();

            //    int result = Calculate(input);

            //    Console.WriteLine($"Результат: {result}");
            //}

            //static int Calculate(string expr)
            //{
            //    int result = 0;
            //    int currentNumber = 0;
            //    char operation = '+';

            //    for (int i = 0; i < expr.Length; i++)
            //    {
            //        char c = expr[i];

            //        if (char.IsDigit(c))
            //        {
            //            currentNumber = currentNumber * 10 + (c - '0');
            //        }


            //        if (!char.IsDigit(c) && c != ' ' || i == expr.Length - 1)
            //        {
            //            if (operation == '+')
            //                result += currentNumber;
            //            else if (operation == '-')
            //                result -= currentNumber;

            //            operation = c;
            //            currentNumber = 0;
            //        }
            //    }

            //    return result;


            //            Завдання 6
            //Користувач з клавіатури вводить деякий текст. Додаток повинен змінювати регістр першої літери кожного речення на букву у верхньому регістрі.
            //Наприклад, якщо користувач ввів:
            //"today is a good day for walking. i will try to walk near the sea".
            //Результат роботи додатка:
            //            Today is a good day for walking.I will try to walk near the sea.



            //    Console.Write("Введіть текст: ");
            //    string text = Console.ReadLine();

            //    string result = CapitalizeSentences(text);

            //    Console.WriteLine("\nРезультат:");
            //    Console.WriteLine(result);
            //}

            //static string CapitalizeSentences(string text)
            //{
            //    char[] chars = text.ToCharArray();
            //    bool newSentence = true;

            //    for (int i = 0; i < chars.Length; i++)
            //    {
            //        if (newSentence && char.IsLetter(chars[i]))
            //        {
            //            chars[i] = char.ToUpper(chars[i]);
            //            newSentence = false;
            //        }

            //        if (chars[i] == '.' || chars[i] == '!' || chars[i] == '?')
            //        {
            //            newSentence = true;
            //        }
            //    }

            //    return new string(chars);

            //            Завдання 7
            //Створіть додаток, що перевіряє текст на неприпустимі слова.Якщо неприпустиме слово знайдено, воно має бути замінено 
            //                на набір символів *.За підсумками роботи додатка необхідно показати статистику дій.
            //Наприклад, якщо і в нас є такий текст:
            //«To be, or not to be, that is the question,
            //Whether 'tis nobler in the mind to suffer
            //The slings and arrows of outrageous fortune,
            //Or to take arms against a sea of troubles,
            //And by opposing end them? To die: to sleep;
            //            No more; and by a sleep to say we end
            //The heart-ache and the thousand natural shocks
            //That flesh is heir to, 'tis a consummation
            //Devoutly to be wish'd. To die, to sleep»
            //Неприпустиме слово:
            //"die".
            //Результат роботи:
            //To be, or not to be, that is the question,
            //Whether 'tis nobler in the mind to suffer
            //The slings and arrows of outrageous fortune,
            //Or to take arms against a sea of troubles,
            //And by opposing end them? To ***: to sleep;
            //            No more; and by a sleep to say we end
            //The heart-ache and the thousand natural shocks
            //That flesh is heir to, 'tis a consummation
            //Devoutly to be wish'd. To ***, to sleep

            //Статистика:
            //            2 заміни слова die.

            //string text =
            //@"To be, or not to be, that is the question,
            //Whether 'tis nobler in the mind to suffer
            //The slings and arrows of outrageous fortune,
            //Or to take arms against a sea of troubles,
            //And by opposing end them? To die: to sleep;
            //No more; and by a sleep to say we end
            //The heart-ache and the thousand natural shocks
            //That flesh is heir to, 'tis a consummation
            //Devoutly to be wish'd. To die, to sleep";

            //            string badWord = "die";

            //            int count = 0;


            //            string pattern = $@"\b{badWord}\b";

            //            string result = Regex.Replace(
            //                text,
            //                pattern,
            //                match =>
            //                {
            //                    count++;
            //                    return new string('*', match.Value.Length);
            //                },
            //                RegexOptions.IgnoreCase
            //            );

            //            Console.WriteLine("Результат:\n");
            //            Console.WriteLine(result);

            //            Console.WriteLine("\nСтатистика:");
            //            Console.WriteLine($"{count} заміни слова \"{badWord}\".");



            //            Завдання 8
            //Реалізуйте програму, яка приймає через командний рядок: рядок для шифрування та ключ зсуву.
            //                Використовуючи шифр Цезаря, зашифруйте рядок і виведіть результат.

            //    if (args.Length < 2)
            //    {
            //        Console.WriteLine("Використання: program.exe \"текст\" зсув");
            //        return;
            //    }

            //    string text = args[0];
            //    int shift;

            //    if (!int.TryParse(args[1], out shift))
            //    {
            //        Console.WriteLine("Помилка: ключ зсуву має бути числом.");
            //        return;
            //    }

            //    string encrypted = CaesarCipher(text, shift);

            //    Console.WriteLine("Зашифрований текст:");
            //    Console.WriteLine(encrypted);
            //}

            //static string CaesarCipher(string text, int shift)
            //{
            //    char[] result = new char[text.Length];

            //    for (int i = 0; i < text.Length; i++)
            //    {
            //        char c = text[i];

            //        if (char.IsLetter(c))
            //        {
            //            char offset = char.IsUpper(c) ? 'A' : 'a';


            //            result[i] = (char)(offset + (c - offset + shift) % 26);


            //            if (result[i] < offset)
            //                result[i] = (char)(result[i] + 26);
            //        }
            //        else
            //        {

            //            result[i] = c;
            //        }
            //    }

            //    return new string(result);

            //            Завдання 9
            //Напишіть програму, яка приймає розміри двовимірного масиву і число для множення через параметри командного рядка(наприклад, 3 3 2).
            //                Програма повинна згенерувати випадковий двовимірний масив, помножити всі його елементи на задане число і вивести результат.


            if (args.Length < 3)
            {
                Console.WriteLine("Використання: program.exe <rows> <cols> <multiplier>");
                return;
            }

            if (!int.TryParse(args[0], out int rows) ||
                !int.TryParse(args[1], out int cols) ||
                !int.TryParse(args[2], out int multiplier))
            {
                Console.WriteLine("Помилка: всі параметри мають бути цілими числами.");
                return;
            }

            int[,] array = new int[rows, cols];
            Random rand = new Random();

            Console.WriteLine("Згенерований масив:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = rand.Next(1, 10);
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] *= multiplier;
                }
            }

            Console.WriteLine($"\nМасив після множення на {multiplier}:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
        
    

