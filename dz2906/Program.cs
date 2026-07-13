using System;
using System.Runtime.InteropServices; 

namespace dz2906
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        const uint MB_YESNO = 0x00000004;
        const uint MB_YESNOCANCEL = 0x00000003;
        const uint MB_ICONQUESTION = 0x00000020;
        const uint MB_ICONINFORMATION = 0x00000400;

        const int IDYES = 6;
        const int IDNO = 7;
        const int IDCANCEL = 2;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Системне програмування: Лабораторна робота ===");
            Console.WriteLine("Гра: Комп'ютер вгадує число за допомогою WinAPI.\n");

            bool playAgain = true;

            while (playAgain)
            {
                MessageBox(IntPtr.Zero, "Загадайте число від 0 до 100 умі, а я спробую його вгадати!", "Гра \"Вгадай число\"", MB_ICONINFORMATION);

                int min = 0;
                int max = 100;
                bool isGuessed = false;

                while (min <= max)
                {
                    int guess = min + (max - min) / 2;

                    int result = MessageBox(IntPtr.Zero,
                        $"Ваше число: {guess}?\n\n[Yes] - Так, вгадав!\n[No] - Моє число БІЛЬШЕ\n[Cancel] - Моє число МЕНШЕ",
                        "Спроба вгадати",
                        MB_YESNOCANCEL | MB_ICONQUESTION);

                    if (result == IDYES)
                    {
                        MessageBox(IntPtr.Zero, $"Ура! Я вгадав число {guess}!", "Перемога!", MB_ICONINFORMATION);
                        isGuessed = true;
                        break;
                    }
                    else if (result == IDNO)
                    {
                        min = guess + 1;
                    }
                    else if (result == IDCANCEL)
                    {
                        max = guess - 1; 
                    }

                    if (min > max && !isGuessed)
                    {
                        MessageBox(IntPtr.Zero, "Здається, ви десь помилилися у відповідях. Спробуймо знову!", "Помилка", MB_ICONINFORMATION);
                        break;
                    }
                }

                int repeatResult = MessageBox(IntPtr.Zero, "Бажаєте зіграти ще раз?", "Повтор гри", MB_YESNO | MB_ICONQUESTION);
                if (repeatResult != IDYES)
                {
                    playAgain = false;
                }
            }

            Console.WriteLine("Програма завершила роботу. Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}