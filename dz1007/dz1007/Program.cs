using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; // або System.Data.SqlClient

namespace SqlServerAsyncApp
{
    class Program
    {
        // Базовий рядок підключення (за замовчуванням до локального екземпляра)
        private static string baseConnectionString = "Server=localhost;Integrated Security=True;TrustServerCertificate=True;";
        private static string currentDatabase = "master";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== АСИНХРОННИЙ КЛІЄНТ SQL SERVER ===");

            // 1. Введення та налаштування рядка підключення
            Console.WriteLine("Введіть свій Connection String (або натисніть Enter для значення за замовчуванням):");
            Console.WriteLine($"За замовчуванням: {baseConnectionString}");
            Console.Write("> ");

            string inputConn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputConn))
            {
                baseConnectionString = inputConn;
            }

            // Перевірка підключення
            if (!await TestConnectionAsync())
            {
                Console.WriteLine("\n[ПОМИЛКА] Не вдалося підключитися до сервера. Перевірте рядок підключення та запуск SQL Server.");
                return;
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine($"\n==========================================");
                Console.WriteLine($" ПОТОЧНА БАЗА ДАНИХ: [{currentDatabase}]");
                Console.WriteLine($"==========================================");
                Console.WriteLine("1. Переглянути список усіх баз даних");
                Console.WriteLine("2. Обрати активну базу даних");
                Console.WriteLine("3. Переглянути список таблиць активної БД");
                Console.WriteLine("4. Переглянути структуру таблиці (поля, типи, PK)");
                Console.WriteLine("5. Виконати довільний SQL-запит (SELECT, INSERT, UPDATE, DELETE)");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть пункт: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        await ListDatabasesAsync();
                        break;
                    case "2":
                        await SelectDatabaseAsync();
                        break;
                    case "3":
                        await ListTablesAsync();
                        break;
                    case "4":
                        await ViewTableStructureAsync();
                        break;
                    case "5":
                        await ExecuteCustomQueryAsync();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        // 1. ПЕРЕВІРКА ПІДКТЮЧЕННЯ
        private static async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var conn = new SqlConnection(GetConnectionString(currentDatabase));
                await conn.OpenAsync(); // Асинхронне відкриття з'єднання
                Console.WriteLine("\n[УСПІХ] Успішно підключено до SQL Server!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА]: {ex.Message}");
                return false;
            }
        }

        // 2. ОТРИМАННЯ СТИСКУ БАЗ ДАНИХ
        private static async Task ListDatabasesAsync()
        {
            string sql = "SELECT name FROM sys.databases WHERE state = 0 ORDER BY name;";

            try
            {
                using var conn = new SqlConnection(GetConnectionString("master"));
                await conn.OpenAsync();

                using var cmd = new SqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync(); // Асинхронне читання

                Console.WriteLine("=== СПИСОК БАЗ ДАНИХ ===");
                while (await reader.ReadAsync())
                {
                    Console.WriteLine($"  • {reader.GetString(0)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ПОМИЛКА]: {ex.Message}");
            }
        }

        // 3. ВИБІР АКТИВНОЇ БАЗИ ДАНИХ
        private static async Task SelectDatabaseAsync()
        {
            Console.Write("Введіть назву бази даних: ");
            string dbName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(dbName)) return;

            try
            {
                // Перевіряємо, чи існує БД, намагаючись до неї підключитися
                using var conn = new SqlConnection(GetConnectionString(dbName));
                await conn.OpenAsync();

                currentDatabase = dbName;
                Console.WriteLine($"[УСПІХ] Активну базу даних змінено на: [{currentDatabase}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ПОМИЛКА]: Не вдалося обрати базу даних '{dbName}'. {ex.Message}");
            }
        }

        // 4. ВІДОБРАЖЕННЯ СПИСКУ ТАБЛИЦЬ
        private static async Task ListTablesAsync()
        {
            string sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;";

            try
            {
                using var conn = new SqlConnection(GetConnectionString(currentDatabase));
                await conn.OpenAsync();

                using var cmd = new SqlCommand(sql, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                Console.WriteLine($"=== ТАБЛИЦІ У БАЗІ ДАНИХ [{currentDatabase}] ===");
                bool hasTables = false;

                while (await reader.ReadAsync())
                {
                    hasTables = true;
                    Console.WriteLine($"  • {reader.GetString(0)}");
                }

                if (!hasTables) Console.WriteLine(" (Таблиць у цій БД не знайдено)");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ПОМИЛКА]: {ex.Message}");
            }
        }

        private static async Task ViewTableStructureAsync()
        {
            Console.Write("Введіть назву таблиці: ");
            string tableName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(tableName)) return;

            string sql = @"
                SELECT 
                    c.COLUMN_NAME, 
                    c.DATA_TYPE, 
                    c.IS_NULLABLE,
                    CASE WHEN k.COLUMN_NAME IS NOT NULL THEN 'YES' ELSE 'NO' END AS IS_PK
                FROM INFORMATION_SCHEMA.COLUMNS c
                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE k 
                    ON c.TABLE_NAME = k.TABLE_NAME 
                    AND c.COLUMN_NAME = k.COLUMN_NAME
                    AND OBJECTPROPERTY(OBJECT_ID(k.CONSTRAINT_SCHEMA + '.' + k.CONSTRAINT_NAME), 'IsPrimaryKey') = 1
                WHERE c.TABLE_NAME = @tableName
                ORDER BY c.ORDINAL_POSITION;";

            try
            {
                using var conn = new SqlConnection(GetConnectionString(currentDatabase));
                await conn.OpenAsync();

                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tableName", tableName);

                using var reader = await cmd.ExecuteReaderAsync();

                Console.WriteLine($"\n=== СТРУКТУРА ТАБЛИЦІ [{tableName}] ===");
                Console.WriteLine($"{"Колонка",-25} | {"Тип даних",-15} | {"Null?",-8} | {"Primary Key",-11}");
                Console.WriteLine(new string('-', 68));

                bool found = false;
                while (await reader.ReadAsync())
                {
                    found = true;
                    string colName = reader.GetString(0);
                    string dataType = reader.GetString(1);
                    string isNullable = reader.GetString(2);
                    string isPk = reader.GetString(3);

                    Console.WriteLine($"{colName,-25} | {dataType,-15} | {isNullable,-8} | {isPk,-11}");
                }

                if (!found) Console.WriteLine($"Таблицю '{tableName}' не знайдено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ПОМИЛКА]: {ex.Message}");
            }
        }

        private static async Task ExecuteCustomQueryAsync()
        {
            Console.WriteLine("Введіть SQL-запит (SELECT, INSERT, UPDATE, DELETE):");
            Console.Write("> ");
            string query = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(query)) return;

            try
            {
                using var conn = new SqlConnection(GetConnectionString(currentDatabase));
                await conn.OpenAsync();

                using var cmd = new SqlCommand(query, conn);

                if (query.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    using var reader = await cmd.ExecuteReaderAsync();

                    Console.WriteLine("\n=== РЕЗУЛЬТАТ ЗАПИТУ ===");

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i)}\t");
                    }
                    Console.WriteLine("\n" + new string('-', 50));

                    int rows = 0;
                    while (await reader.ReadAsync())
                    {
                        rows++;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader.GetValue(i)}\t");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine($"\nВсього рядків: {rows}");
                }
                else
                {
                    int affectedRows = await cmd.ExecuteNonQueryAsync();
                    Console.WriteLine($"\n[УСПІХ] Запит виконано. Оброблено/змінено рядків: {affectedRows}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ПОМИЛКА SQL]: {ex.Message}");
            }
        }

        private static string GetConnectionString(string dbName)
        {
            var builder = new SqlConnectionStringBuilder(baseConnectionString)
            {
                InitialCatalog = dbName
            };
            return builder.ConnectionString;
        }
    }
}