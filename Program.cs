using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PersonalFinanceApp
{
    internal class Program
    {
        private static User? _currentUser = null;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            using (var db = new FinanceContext())
            {
                CreateStoredProcedureIfNotExist(db);

                LoginOrCreateUser(db);

                bool exit = false;
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine($"=== ОБЛІК ФІНАНСІВ | Користувач: {_currentUser!.Username} ===");
                    Console.WriteLine("1. Додати транзакцію (Дохід / Витрату)");
                    Console.WriteLine("2. Переглянути список усіх транзакцій");
                    Console.WriteLine("3. Розрахунок доходов та витрат за період");
                    Console.WriteLine("4. Фільтрувати транзакції (тип + період)");
                    Console.WriteLine("5. Звіт про стан фінансів (статистика за категоріями)");
                    Console.WriteLine("6. Спеціальні SQL команди (Прямий SQL + Процедура)");
                    Console.WriteLine("0. Вихід");
                    Console.Write("\nОберіть дію: ");

                    string? choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddTransaction(db);
                            break;
                        case "2":
                            ViewTransactions(db);
                            break;
                        case "3":
                            CalculateTotals(db);
                            break;
                        case "4":
                            FilterTransactions(db);
                            break;
                        case "5":
                            ShowFinancialReport(db);
                            break;
                        case "6":
                            ExecuteRawSqlAndProcedures(db);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        private static void LoginOrCreateUser(FinanceContext db)
        {
            Console.WriteLine("=== ЛАСКАВО ПРОСИМО ДО СИСТЕМИ ОБЛІКУ ФІНАНСІВ ===");
            Console.Write("Введіть ваше ім'я користувача (Username): ");
            string username = Console.ReadLine()?.Trim() ?? "User";

            var user = db.Users.Include(u => u.ProfileSetting).FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                Console.WriteLine($"Користувача '{username}' не знайдено. Створюємо новий профіль...");
                Console.Write("Введіть ваш Email: ");
                string email = Console.ReadLine()?.Trim() ?? "email@example.com";

                user = new User
                {
                    Username = username,
                    Email = email,
                    ProfileSetting = new ProfileSetting { PreferredCurrency = "UAH", EnableNotifications = true }
                };

                db.Users.Add(user);
                db.SaveChanges();
                Console.WriteLine("Новий користувач успішно зареєстрований!");
            }
            else
            {
                Console.WriteLine($"З поверненням, {user.Username}!");
            }

            _currentUser = user;
            Console.WriteLine("Натисніть будь-яку клавішу для переходу в головне меню...");
            Console.ReadKey();
        }

        private static void AddTransaction(FinanceContext db)
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАВАННЯ НОВОЇ ТРАНЗАКЦІЇ ===");

            Console.WriteLine("1. Дохід (Income)");
            Console.WriteLine("2. Витрата (Expense)");
            Console.Write("Оберіть тип: ");
            string typeChoice = Console.ReadLine() ?? "2";
            TransactionType type = typeChoice == "1" ? TransactionType.Income : TransactionType.Expense;

            Console.Write("Введіть суму (наприклад, 250,50): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Помилка: Некоректна сума транзакції.");
                Console.ReadKey();
                return;
            }

            Console.Write("Введіть опис: ");
            string description = Console.ReadLine() ?? "Без опису";

            Console.WriteLine("\nДоступні категорії:");
            var categories = db.Categories.ToList();
            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.Id}. {cat.Name}");
            }
            Console.Write("Оберіть ID категорії: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId) || !categories.Any(c => c.Id == categoryId))
            {
                categoryId = 1;
            }

            var transaction = new Transaction
            {
                Amount = amount,
                Description = description,
                Type = type,
                Date = DateTime.Now,
                UserId = _currentUser!.Id,
                CategoryId = categoryId
            };

            db.Transactions.Add(transaction);
            db.SaveChanges();

            Console.WriteLine("\nТранзакцію успішно додано до бази даних!");
            Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey();
        }

        private static void ViewTransactions(FinanceContext db)
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК УСІХ ВАШИХ ТРАНЗАКЦІЙ ===");

            var transactions = db.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == _currentUser!.Id)
                .OrderByDescending(t => t.Date)
                .ToList();

            if (!transactions.Any())
            {
                Console.WriteLine("У вас ще немає доданих транзакцій.");
            }
            else
            {
                Console.WriteLine("-----------------------------------------------------------------------------");
                Console.WriteLine($"{"Дата",-12} | {"Тип",-7} | {"Сума",-10} | {"Категорія",-12} | {"Опис"}");
                Console.WriteLine("-----------------------------------------------------------------------------");

                foreach (var t in transactions)
                {
                    string typeStr = t.Type == TransactionType.Income ? "Дохід" : "Витрата";
                    Console.WriteLine($"{t.Date.ToString("dd.MM.yyyy"),-12} | {typeStr,-7} | {t.Amount,-10:C2} | {t.Category.Name,-12} | {t.Description}");
                }
                Console.WriteLine("-----------------------------------------------------------------------------");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey();
        }

        private static void CalculateTotals(FinanceContext db)
        {
            Console.Clear();
            Console.WriteLine("=== РОЗРАХУНОК ДОХОДІВ ТА ВИТРАТ ЗА ПЕРІОД ===");

            Console.Write("Введіть початкову дату (дд.мм.рррр): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) return;

            Console.Write("Введіть кінцеву дату (дд.мм.рррр): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate)) return;

            endDate = endDate.Date.AddDays(1).AddTicks(-1);

            var userTransactions = db.Transactions
                .Where(t => t.UserId == _currentUser!.Id && t.Date >= startDate && t.Date <= endDate);

            decimal totalIncome = userTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => (decimal?)t.Amount) ?? 0;
            decimal totalExpense = userTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => (decimal?)t.Amount) ?? 0;

            Console.WriteLine($"\nПеріод: з {startDate.ToString("dd.MM.yyyy")} по {endDate.ToString("dd.MM.yyyy")}");
            Console.WriteLine("-----------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Загальний ДОХІД : {totalIncome:C2}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Загальні ВИТРАТИ: {totalExpense:C2}");
            Console.ResetColor();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"Чистий баланс: {(totalIncome - totalExpense):C2}");

            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey();
        }

        private static void FilterTransactions(FinanceContext db)
        {
            Console.Clear();
            Console.WriteLine("=== ФІЛЬТРУВАННЯ ТРАНЗАКЦІЙ ===");

            Console.WriteLine("Оберіть тип для фільтрації:");
            Console.WriteLine("1. Тільки доходи");
            Console.WriteLine("2. Тільки витрати");
            Console.Write("Ваш вибір: ");
            string filterChoice = Console.ReadLine() ?? "1";
            TransactionType targetType = filterChoice == "1" ? TransactionType.Income : TransactionType.Expense;

            Console.Write("Введіть початкову дату періоду (дд.мм.рррр): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime start)) return;

            Console.Write("Введіть кінцеву дату періоду (дд.мм.рррр): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime end)) return;
            end = end.Date.AddDays(1).AddTicks(-1);

            var filtered = db.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == _currentUser!.Id)
                .Where(t => t.Type == targetType)
                .Where(t => t.Date >= start && t.Date <= end)
                .OrderBy(t => t.Date)
                .ToList();

            Console.WriteLine($"\nРезультати фільтрації (Тип: {(targetType == TransactionType.Income ? "Дохід" : "Витрата")}):");
            Console.WriteLine("-----------------------------------------------------------------------------");
            foreach (var t in filtered)
            {
                Console.WriteLine($"{t.Date.ToString("dd.MM.yyyy"),-12} | {t.Amount,-10:C2} | {t.Category.Name,-12} | {t.Description}");
            }
            Console.WriteLine("-----------------------------------------------------------------------------");

            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey();
        }

        private static void ShowFinancialReport(FinanceContext db)
        {
            Console.Clear();
            Console.WriteLine("=== ФІНАНСОВИЙ ЗВІТ ТА СТАТИСТИКА ===");

            var allTransactions = db.Transactions.Where(t => t.UserId == _currentUser!.Id).ToList();

            decimal totalIncome = allTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            decimal totalExpense = allTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
            decimal balance = totalIncome - totalExpense;

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"Загальний зароблений дохід: {totalIncome:C2}");
            Console.WriteLine($"Загальні витрати:           {totalExpense:C2}");
            Console.WriteLine($"Поточний загальний баланс:  {balance:C2}");
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("\nСТАТИСТИКА ВИТРАТ ЗА КАТЕГОРІЯМИ:");
            var categoryStats = db.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == _currentUser!.Id && t.Type == TransactionType.Expense)
                .GroupBy(t => t.Category.Name)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    TotalAmount = g.Sum(t => t.Amount),
                    Count = g.Count()
                })
                .OrderByDescending(x => x.TotalAmount)
                .ToList();

            if (!categoryStats.Any())
            {
                Console.WriteLine("Немає даних про витрати для формування статистики.");
            }
            else
            {
                foreach (var stat in categoryStats)
                {
                    Console.WriteLine($"- {stat.CategoryName,-12}: {stat.TotalAmount,-10:C2} (Транзакцій: {stat.Count})");
                }
            }
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey();
        }

        private static void ExecuteRawSqlAndProcedures(FinanceContext db)
        {
            Console.Clear();
            Console.WriteLine("=== СПЕЦІАЛЬНИЙ РЕЖИМ (RAW SQL & STORED PROCEDURES) ===");

            Console.WriteLine("\n1. Топ-3 найбільших операцій за сумою (Виклик через Чистий SQL):");

            var topTransactions = db.Transactions
                .FromSqlRaw("SELECT TOP 3 * FROM Transactions WHERE UserId = {0} ORDER BY Amount DESC", _currentUser!.Id)
                .ToList();

            foreach (var t in topTransactions)
            {
                string tType = t.Type == TransactionType.Income ? "Дохід" : "Витрата";
                Console.WriteLine($"- {t.Date.ToString("dd.MM.yyyy")}: {tType} на суму {t.Amount:C2} ({t.Description})");
            }

            Console.WriteLine("\n2. Статистика всієї системи (Виклик Зберігаючої Процедури):");
            try
            {
                var totalUsers = db.Users
                    .FromSqlRaw("EXEC GetTotalUsersCount")
                    .IgnoreQueryFilters() 
                    .Count();

                Console.WriteLine($"[ПРОЦЕДУРА]: Зараз у базі даних зареєстровано користувачів: {totalUsers}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка виклику процедури: {ex.Message}");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
            Console.ReadKey();
        }

        private static void CreateStoredProcedureIfNotExist(FinanceContext db)
        {
            try
            {
                string checkAndCreateScript = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTotalUsersCount]') AND type in (N'P', N'PC'))
                BEGIN
                    EXEC dt_activate_objects 'CREATE PROCEDURE [dbo].[GetTotalUsersCount] AS BEGIN SELECT * FROM Users END'
                END";

                db.Database.ExecuteSqlRaw(@"
                    IF OBJECT_ID('GetTotalUsersCount', 'P') IS NULL
                    BEGIN
                        EXEC('CREATE PROCEDURE GetTotalUsersCount AS BEGIN SELECT * FROM Users END')
                    END");
            }
            catch { }
        }
    }
}