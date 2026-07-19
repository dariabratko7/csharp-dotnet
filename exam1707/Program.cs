using System;
using System.IO;
using System.Reflection;

namespace exam1707
{
    class Program
    {
        static void Main(string[] args)
        { 
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
             
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataValidationLib.dll");

            if (!File.Exists(dllPath))
            {
                Console.WriteLine($"[Помилка] Не знайдено файл бібліотеки за шляхом: {dllPath}");
                Console.WriteLine("Будь ласка, скопіюйте файл DataValidationLib.dll у папку з цією програмою.");
                return;
            }

            try
            { 
                Assembly assembly = Assembly.LoadFrom(dllPath);
                 
                Type validatorType = assembly.GetType("DataValidationLib.Validator");

                if (validatorType == null)
                {
                    Console.WriteLine("Клас DataValidationLib.Validator не знайдено в DLL.");
                    return;
                }

                Console.WriteLine("=== Екзамен: Динамічна валідація даних ===\n");
                 
                Console.Write("Введіть ПІБ: ");
                string name = Console.ReadLine();

                Console.Write("Введіть вік: ");
                string age = Console.ReadLine();

                Console.Write("Введіть телефон (наприклад, +380671234567): ");
                string phone = Console.ReadLine();

                Console.Write("Введіть Email: ");
                string email = Console.ReadLine();

                Console.WriteLine("\n--- Результати перевірки (виклики через Reflection) ---");
                 
                bool isNameValid = (bool)validatorType.InvokeMember("ValidateFullName",
                    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                    null, null, new object[] { name });

                bool isAgeValid = (bool)validatorType.InvokeMember("ValidateAge",
                    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                    null, null, new object[] { age });

                bool isPhoneValid = (bool)validatorType.InvokeMember("ValidatePhone",
                    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                    null, null, new object[] { phone });

                bool isEmailValid = (bool)validatorType.InvokeMember("ValidateEmail",
                    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                    null, null, new object[] { email });
                 
                PrintResult("ПІБ (тільки літери)", isNameValid);
                PrintResult("Вік (тільки цифри)", isAgeValid);
                PrintResult("Телефон (формат номера)", isPhoneValid);
                PrintResult("Email (формат адреси)", isEmailValid);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Сталася помилка при виконанні: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
         
        static void PrintResult(string fieldName, bool isValid)
        {
            if (isValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[УСПІХ] {fieldName}: Дані коректні.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ПОМИЛКА] {fieldName}: Невірний формат!");
            }
            Console.ResetColor();
        }
    }
}