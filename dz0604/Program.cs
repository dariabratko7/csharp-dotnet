using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz0604
{
    //    Визначте клас «Matrix», який представляє двовимірну матрицю.Реалізуйте індексатор,
    //        який дозволить отримати доступ до елементів матриці за індексами[row, column]. 
    //        Додати властивості Rows і Cols, доступні для читання.Заповніть та відобразіть 
    //        об'єкт класу Matrix, використовуючи цикл for.
    //Визначте у класі, такі методи:
    //1) Метод, що повертає найбільше значення матриці.
    //2) Метод, що повертає найменше значення матриці.
    //3) Метод приймає як параметр об'єкт класу Matrix і повертає добуток двох матриць.

    //class Matrix
    //{
    //    private int[,] data;

    //    public int Rows { get; }
    //    public int Cols { get; }

    //    public Matrix(int rows, int cols)
    //    {
    //        Rows = rows;
    //        Cols = cols;
    //        data = new int[rows, cols];
    //    }

    //    public int this[int row, int col]
    //    {
    //        get { return data[row, col]; }
    //        set { data[row, col] = value; }
    //    }

    //    public int GetMax()
    //    {
    //        int max = data[0, 0];

    //        for (int i = 0; i < Rows; i++)
    //            for (int j = 0; j < Cols; j++)
    //                if (data[i, j] > max)
    //                    max = data[i, j];

    //        return max;
    //    }

    //    public int GetMin()
    //    {
    //        int min = data[0, 0];

    //        for (int i = 0; i < Rows; i++)
    //            for (int j = 0; j < Cols; j++)
    //                if (data[i, j] < min)
    //                    min = data[i, j];

    //        return min;
    //    }

    //    public Matrix Multiply(Matrix other)
    //    {
    //        if (this.Cols != other.Rows)
    //            throw new Exception("Неможливо перемножити матриці!");

    //        Matrix result = new Matrix(this.Rows, other.Cols);

    //        for (int i = 0; i < this.Rows; i++)
    //        {
    //            for (int j = 0; j < other.Cols; j++)
    //            {
    //                for (int k = 0; k < this.Cols; k++)
    //                {
    //                    result[i, j] += this[i, k] * other[k, j];
    //                }
    //            }
    //        }

    //        return result;
    //    }

    //    public void Print()
    //    {
    //        for (int i = 0; i < Rows; i++)
    //        {
    //            for (int j = 0; j < Cols; j++)
    //            {
    //                Console.Write(this[i, j] + "\t");
    //            }
    //            Console.WriteLine();
    //        }
    //    }
    //}

    //class Program
    //{
    //    static void Main()
    //    {
    //        Matrix m1 = new Matrix(2, 2);
    //        Matrix m2 = new Matrix(2, 2);

    //        Console.WriteLine("Введіть елементи першої матриці:");
    //        for (int i = 0; i < m1.Rows; i++)
    //        {
    //            for (int j = 0; j < m1.Cols; j++)
    //            {
    //                Console.Write($"[{i},{j}]: ");
    //                m1[i, j] = int.Parse(Console.ReadLine());
    //            }
    //        }

    //        Console.WriteLine("\nВведіть елементи другої матриці:");
    //        for (int i = 0; i < m2.Rows; i++)
    //        {
    //            for (int j = 0; j < m2.Cols; j++)
    //            {
    //                Console.Write($"[{i},{j}]: ");
    //                m2[i, j] = int.Parse(Console.ReadLine());
    //            }
    //        }

    //        Console.WriteLine("\nМатриця 1:");
    //        m1.Print();

    //        Console.WriteLine("\nМатриця 2:");
    //        m2.Print();

    //        Console.WriteLine($"\nМаксимум m1: {m1.GetMax()}");
    //        Console.WriteLine($"Мінімум m1: {m1.GetMin()}");

    //        try
    //        {
    //            Matrix result = m1.Multiply(m2);
    //            Console.WriteLine("\nРезультат множення:");
    //            result.Print();
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //        }
    //    }
    //}


    //    Розробити клас для представлення об'єкта рядок використовуйте масив char.
    //        Визначте конструктор для прийняття рядка, конструктор з одним параметром цілого типу –
    //        довжина рядка, який можна використовувати як конструктор замовчуванням. 
    //Визначте конструктор, який копіює у новий рядок n перші символи іншого рядка і які можна використовувати як конструктор копіювання.
    //Визначте  індексатор і властивість Length, для повернення довжини рядки. Метод Main має виглядати так:
    //static void Main()
    //    {
    //        String str = new String("Hello world");
    //        Console.WriteLine($"String: {str}. Length: {str.Length}");
    //        String str2 = new String(str, 6);
    //        Console.WriteLine($"String: {str2}. Length: {str2.Length}");
    //        String str3 = new String(10);
    //        Console.WriteLine($"String: {str3}. Length: {str3.Length}");
    //    }

    class String
    {
        private char[] data;

        public String(System.String str)
        {
            data = str.ToCharArray();
        }

        public String(int length)
        {
            data = new char[length];
            for (int i = 0; i < length; i++)
                data[i] = ' ';
        }

        public String(String other, int n)
        {
            int len = Math.Min(n, other.Length);
            data = new char[len];

            for (int i = 0; i < len; i++)
                data[i] = other[i];
        }

        public char this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        public int Length
        {
            get { return data.Length; }
        }

        public override System.String ToString()
        {
            return new System.String(data);
        }
    }

    class Program
    {
        static void Main()
        {
            String str = new String("Hello world");
            Console.WriteLine($"String: {str}. Length: {str.Length}");

            String str2 = new String(str, 6);
            Console.WriteLine($"String: {str2}. Length: {str2.Length}");

            String str3 = new String(10);
            Console.WriteLine($"String: {str3}. Length: {str3.Length}");
        }
    }

    //Існує алгоритмічна гра “Подвоювач”. У цій грі людині пропонується якесь число,
    //    а людина повинна, керуючи роботом “Подвоювач”, досягти цього числа за мінімальну 
    //    кількість кроків.Робот вміє виконувати кілька команд: збільшити число на 1, 
    //    помножити число на 2 та скинути число до 1.Початкове значення подвоювача дорівнює 1.
    //    Реалізувати клас “Подвоювач”. Клас зберігає у собі поле current -поточне значення,
    //    finish - число, якого потрібно досягти, конструктор, у якому задається кінцеве число.
    //    Методи: збільшити число на 1, збільшити число вдвічі, скидання поточного до 1,
    //    властивість Current, яка повертає поточне значення, властивість Finish,
    //    що повертає кінцеве значення.Створити за допомогою цього класу гру, де комп'ютер 
    //    загадує число, а людина. вибираючи з меню на екрані, віддає команди подвоювачу і
    //    намагається отримати це число за найменшу кількість ходів. 
    //    Якщо людина отримує кількість більше, ніж належить, гра припиняється.
    class Doubler
    {
        private int current;
        private int finish;

        public Doubler(int finish)
        {
            this.finish = finish;
            current = 1;
        }

        public void AddOne()
        {
            current++;
        }

        public void MultiplyByTwo()
        {
            current *= 2;
        }

        public void Reset()
        {
            current = 1;
        }

        public int Current
        {
            get { return current; }
        }

        public int Finish
        {
            get { return finish; }
        }
    
       static void Main()
        {
            Random rand = new Random();
            int target = rand.Next(10, 101);

            Doubler game = new Doubler(target);

            Console.WriteLine($"Загадане число: {game.Finish}");

            int minSteps = CalculateMinSteps(game.Finish);
            int steps = 0;

            while (true)
            {
                Console.WriteLine($"\nПоточне число: {game.Current}");
                Console.WriteLine("1 - +1");
                Console.WriteLine("2 - *2");
                Console.WriteLine("3 - Скинути до 1");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        game.AddOne();
                        steps++;
                        break;
                    case "2":
                        game.MultiplyByTwo();
                        steps++;
                        break;
                    case "3":
                        game.Reset();
                        steps++;
                        break;
                    default:
                        Console.WriteLine("Невірна команда!");
                        continue;
                }

                if (game.Current == game.Finish)
                {
                    Console.WriteLine($"\n Ви виграли за {steps} ходів!");
                    Console.WriteLine($"Мінімально можливі ходи: {minSteps}");
                    break;
                }

                if (steps > minSteps)
                {
                    Console.WriteLine("\n Ви перевищили мінімальну кількість ходів. Гру завершено.");
                    Console.WriteLine($"Потрібно було: {minSteps}");
                    break;
                }
            }
        }

        static int CalculateMinSteps(int n)
        {
            int steps = 0;

            while (n > 1)
            {
                if (n % 2 == 0)
                    n /= 2;
                else
                    n -= 1;

                steps++;
            }

            return steps;
        }
    }
}