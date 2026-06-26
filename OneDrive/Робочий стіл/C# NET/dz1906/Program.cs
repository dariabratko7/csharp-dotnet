using System;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        CreateStoredProcedures();

        Console.WriteLine("\n=================== ВИКОНАННЯ ВИКЛИКУ ПРОЦЕДУР ===================\n");

        using (var context = new ApplicationContext())
        {
            int userIdParam = 1;
            var user1 = context.Users
                .FromSqlRaw("EXEC GetUserById @UserId", new SqlParameter("@UserId", userIdParam))
                .AsEnumerable()
                .FirstOrDefault();
            Console.WriteLine($"1) Інфо про користувача з Id {userIdParam}: {(user1 != null ? $"{user1.Name} ({user1.Email})" : "Не знайдено")}");


            var activeUsers = context.Users
                .FromSqlRaw("EXEC GetActiveUsers")
                .ToList();
            Console.WriteLine($"2) Активні користувачі: " + string.Join(", ", activeUsers.Select(u => u.Name)));


            var nameArg = new SqlParameter("@Name", "Dmytro Kozak");
            var emailArg = new SqlParameter("@Email", "dmytro@example.com");
            var isActiveArg = new SqlParameter("@IsActive", true);
            var regDateArg = new SqlParameter("@RegDate", DateTime.Now);
            var birthDateArg = new SqlParameter("@BirthDate", new DateTime(2002, 7, 8));

            context.Database.ExecuteSqlRaw("EXEC AddUser @Name, @Email, @IsActive, @RegDate, @BirthDate",
                nameArg, emailArg, isActiveArg, regDateArg, birthDateArg);
            Console.WriteLine("3) Процедура AddUser успішно виконалася (користувача додано).");


            int updateUserId = 2;
            string newEmail = "alice_new@example.com";
            context.Database.ExecuteSqlRaw("EXEC UpdateUserEmail @UserId, @NewEmail",
                new SqlParameter("@UserId", updateUserId),
                new SqlParameter("@NewEmail", newEmail));
            Console.WriteLine($"4) Email користувача з Id {updateUserId} оновлено на: {newEmail}");


            int deleteUserId = 3;
            context.Database.ExecuteSqlRaw("EXEC DeleteUser @UserId", new SqlParameter("@UserId", deleteUserId));
            Console.WriteLine($"5) Процедуру видалення користувача з Id {deleteUserId} виконано.");


            var count = context.Database.ExecuteSqlRaw("EXEC GetActiveUsersCount");
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXEC GetActiveUsersCount";
                context.Database.OpenConnection();
                int activeCount = (int)command.ExecuteScalar();
                Console.WriteLine($"6) Кількість активних користувачів у базі: {activeCount}");
            }


            var startDate = new SqlParameter("@StartDate", new DateTime(2022, 1, 1));
            var endDate = new SqlParameter("@EndDate", new DateTime(2023, 12, 31));
            var usersInRange = context.Users
                .FromSqlRaw("EXEC GetUsersByRegistrationRange @StartDate, @EndDate", startDate, endDate)
                .ToList();
            Console.WriteLine("7) Користувачі, зареєстровані у 2022-2023 роках: " + string.Join(", ", usersInRange.Select(u => u.Name)));


            var outputParam = new SqlParameter
            {
                ParameterName = "@AvgAge",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
            context.Database.ExecuteSqlRaw("EXEC GetAverageAge @AvgAge OUTPUT", outputParam);
            Console.WriteLine($"8) Середній вік користувачів (через OUTPUT параметр): {outputParam.Value} років");


            int orderUserId = 1;
            var ordersOfUser = context.Orders
                .FromSqlRaw("EXEC GetOrdersByUserId @UserId", new SqlParameter("@UserId", orderUserId))
                .ToList();
            Console.WriteLine($"9) Замовлення користувача з Id {orderUserId}: " + string.Join(", ", ordersOfUser.Select(o => $"Замовлення №{o.Id} на суму {o.TotalAmount} грн")));


            decimal maxPriceParam = 200.00m;
            var cheapProducts = context.Products
                .FromSqlRaw("EXEC GetProductsByMaxPrice @MaxPrice", new SqlParameter("@MaxPrice", maxPriceParam))
                .ToList();
            Console.WriteLine($"10) Товари з ціною до {maxPriceParam} грн: " + string.Join(", ", cheapProducts.Select(p => $"{p.Name} ({p.Price} грн)")));


            int shortUserId = 1;
            var shortOrders = context.Orders
                .FromSqlRaw("EXEC GetUserOrdersShort @UserId", new SqlParameter("@UserId", shortUserId))
                .ToList();
            Console.WriteLine($"11) Скорочений список замовлень користувача {shortUserId}:");
            foreach (var o in shortOrders)
            {
                Console.WriteLine($"    - Id: {o.Id}, Дата: {o.OrderDate.ToShortDateString()}, Сума: {o.TotalAmount}");
            }


            var expensiveOrder = context.Orders
                .FromSqlRaw("EXEC GetMostExpensiveOrder")
                .AsEnumerable()
                .FirstOrDefault();
            Console.WriteLine($"12) Найдорожче замовлення в базі: Замовлення №{expensiveOrder?.Id} на суму {expensiveOrder?.TotalAmount} грн");
        }

        Console.WriteLine("\n==================================================================");
        Console.ReadLine();
    }

    static void CreateStoredProcedures()
    {
        using (var context = new ApplicationContext())
        {
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetUserById @UserId INT AS BEGIN SELECT * FROM Users WHERE Id = @UserId; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetActiveUsers AS BEGIN SELECT * FROM Users WHERE IsActive = 1; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE AddUser @Name NVARCHAR(MAX), @Email NVARCHAR(MAX), @IsActive BIT, @RegDate DATETIME, @BirthDate DATETIME AS BEGIN INSERT INTO Users (Name, Email, IsActive, RegistrationDate, BirthDate) VALUES (@Name, @Email, @IsActive, @RegDate, @BirthDate); END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE UpdateUserEmail @UserId INT, @NewEmail NVARCHAR(MAX) AS BEGIN UPDATE Users SET Email = @NewEmail WHERE Id = @UserId; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE DeleteUser @UserId INT AS BEGIN DELETE FROM Users WHERE Id = @UserId; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetActiveUsersCount AS BEGIN SELECT COUNT(*) FROM Users WHERE IsActive = 1; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetUsersByRegistrationRange @StartDate DATETIME, @EndDate DATETIME AS BEGIN SELECT * FROM Users WHERE RegistrationDate BETWEEN @StartDate AND @EndDate; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetAverageAge @AvgAge INT OUTPUT AS BEGIN SELECT @AvgAge = AVG(DATEDIFF(year, BirthDate, GETDATE())) FROM Users; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetOrdersByUserId @UserId INT AS BEGIN SELECT * FROM Orders WHERE UserId = @UserId; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetProductsByMaxPrice @MaxPrice DECIMAL(18,2) = NULL AS BEGIN SELECT * FROM Products WHERE @MaxPrice IS NULL OR Price <= @MaxPrice; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetUserOrdersShort @UserId INT AS BEGIN SELECT Id, OrderDate, TotalAmount FROM Orders WHERE UserId = @UserId; END");
            context.Database.ExecuteSqlRaw(@"CREATE OR ALTER PROCEDURE GetMostExpensiveOrder AS BEGIN SELECT TOP 1 * FROM Orders ORDER BY TotalAmount DESC; END");
        }
    }
}