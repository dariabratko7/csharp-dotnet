using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Напишіть узагальнений клас, який може зберігати у масиві об'єкти будь-якого типу. Крім того, даний клас повинен мати методи для додавання даних до масиву,
//    видалення з масиву, отримання елемента з масиву за індексом та метод, що повертає довжину масиву. Для спрощення роботи можна перестворювати масив
//    при кожній операції додавання та видалення.

class MyArray<T>
{
    private T[] data;
    private int size;

    public MyArray()
    {
        data = new T[0];
        size = 0;
    }

    public void Add(T value)
    {
        T[] newData = new T[size + 1];

        for (int i = 0; i < size; i++)
        {
            newData[i] = data[i];
        }

        newData[size] = value;

        data = newData;
        size++;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= size)
        {
            Console.WriteLine("Невірний індекс!");
            return;
        }

        T[] newData = new T[size - 1];

        for (int i = 0, j = 0; i < size; i++)
        {
            if (i != index)
            {
                newData[j++] = data[i];
            }
        }

        data = newData;
        size--;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= size)
            throw new IndexOutOfRangeException("Невірний індекс!");

        return data[index];
    }

    public int Length()
    {
        return size;
    }

    public void Print()
    {
        foreach (var item in data)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        MyArray<int> arr = new MyArray<int>();

        arr.Add(10);
        arr.Add(20);
        arr.Add(30);

        arr.Print();

        arr.RemoveAt(1);
        arr.Print();

        Console.WriteLine("Елемент [1]: " + arr.Get(1));
        Console.WriteLine("Довжина: " + arr.Length());
    }
}

//Розробити власний універсальний клас, що містить метод обчислення найменших значень елементів рядків матриці розміру [m, n]. 
//    Результатом має бути одновимірний масив, розмірністю [m]. Матриця може мати будь-який тип даних.Розробити власний універсальний клас,
//    що містить метод обчислення найменших значень елементів рядків матриці розміру [m, n]. 
//    Результатом має бути одновимірний масив, розмірністю [m]. Матриця може мати будь-який тип даних.

class MatrixHelper<T> where T : IComparable<T>
{
    private T[,] matrix;
    private int m;
    private int n;

    public MatrixHelper(T[,] matrix)
    {
        this.matrix = matrix;
        m = matrix.GetLength(0);
        n = matrix.GetLength(1);
    }

    public T[] GetRowMinimums()
    {
        T[] result = new T[m];

        for (int i = 0; i < m; i++)
        {
            T min = matrix[i, 0];

            for (int j = 1; j < n; j++)
            {
                if (matrix[i, j].CompareTo(min) < 0)
                {
                    min = matrix[i, j];
                }
            }

            result[i] = min;
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        int[,] matrix = {
            { 5, 2, 8 },
            { 9, 1, 4 },
            { 7, 3, 6 }
        };

        MatrixHelper<int> helper = new MatrixHelper<int>(matrix);

        int[] mins = helper.GetRowMinimums();

        Console.WriteLine("Мінімуми по рядках:");
        foreach (var x in mins)
        {
            Console.Write(x + " ");
        }
    }
}

//Створити універсальний клас Item<T, T2> з властивостями: Index, Value.
//    Створити універсальний клас Collection<T, T2> з List<Item<T, T2>>.
//    Реалізувати можливість додавання елементів до колекції класу.

public class Item<T, T2>
{
    public T Index { get; set; }
    public T2 Value { get; set; }

    public Item(T index, T2 value)
    {
        Index = index;
        Value = value;
    }

    public override string ToString()
    {
        return $"Index: {Index}, Value: {Value}";
    }
}

public class Collection<T, T2>
{
    private List<Item<T, T2>> items = new List<Item<T, T2>>();

    public void Add(T index, T2 value)
    {
        items.Add(new Item<T, T2>(index, value));
    }

    public void Add(Item<T, T2> item)
    {
        items.Add(item);
    }

    public void Print()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}

class Program
{
    static void Main()
    {
        Collection<int, string> collection = new Collection<int, string>();

        collection.Add(1, "Один");
        collection.Add(2, "Два");
        collection.Add(new Item<int, string>(3, "Три"));

        collection.Print();
    }
}

