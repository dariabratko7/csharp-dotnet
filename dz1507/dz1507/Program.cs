using System;
using Microsoft.Win32;

namespace RegistryDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string subKeyPath = @"Software\Values";

            Console.WriteLine("=== РОБОТА З РЕЄСТРОМ WINDOWS ===\n");

            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(subKeyPath))
                {
                    if (key != null)
                    {
                        Console.WriteLine($"1. Створено розділ в HKCU:\\{subKeyPath}");

                        long qwordValue = 987654321098765L;
                        key.SetValue("MyQWordParam", qwordValue, RegistryValueKind.QWord);

                        string stringValue = "Тестове значення для реєстру";
                        key.SetValue("MyStringParam", stringValue, RegistryValueKind.String);

                        string[] arrayValue = new string[] { "Значення 1", "Значення 2", "Значення 3" };
                        key.SetValue("MyStringArrayParam", arrayValue, RegistryValueKind.MultiString);

                        Console.WriteLine("2. Усі 3 параметри успішно записано.\n");
                    }
                }

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath))
                {
                    if (key != null)
                    {
                        Console.WriteLine("--- ЗЧИТАНІ ЗНАЧЕННЯ ---");

                        object rawQWord = key.GetValue("MyQWordParam");
                        long readQWord = Convert.ToInt64(rawQWord);
                        Console.WriteLine($"1) QWord (MyQWordParam): {readQWord}");

                        string readString = key.GetValue("MyStringParam") as string;
                        Console.WriteLine($"2) String (MyStringParam): \"{readString}\"");

                        string[] readArray = key.GetValue("MyStringArrayParam") as string[];
                        Console.WriteLine("3) String[] (MyStringArrayParam):");
                        if (readArray != null)
                        {
                            foreach (string item in readArray)
                            {
                                Console.WriteLine($"    - {item}");
                            }
                        }
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("--- ОЧИЩЕННЯ РЕЄСТРУ ---");

                Registry.CurrentUser.DeleteSubKeyTree(subKeyPath);
                Console.WriteLine($"4. Розділ '{subKeyPath}' та всі його параметри успішно видалено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка під час роботи з реєстром: {ex.Message}");
            }
        }
    }
}