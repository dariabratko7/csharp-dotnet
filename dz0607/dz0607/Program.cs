using System;
using System.Threading;

namespace BankAtmSimulation
{
    class BankAccount
    {
        private decimal _balance;

        private readonly object _lockObject = new object();

        public BankAccount(decimal initialBalance)
        {
            _balance = initialBalance;
        }

        public void Withdraw(string atmName, string customerName, decimal amount)
        {
            Console.WriteLine($"[{atmName}] Клієнт {customerName} прийшов зняти {amount} грн.");

            bool lockTaken = false;

            try
            {
                Monitor.Enter(_lockObject, ref lockTaken);

                Console.WriteLine($"[{atmName}] ---> Перевірка балансу для {customerName}. Доступно: {_balance} грн.");

                Thread.Sleep(1000);

                if (_balance >= amount)
                {
                    _balance -= amount;
                    Console.WriteLine($"[{atmName}] [УСПІХ] {customerName} отримав {amount} грн. Залишок: {_balance} грн.\n");
                }
                else
                {
                    Console.WriteLine($"[{atmName}] [ВІДМОВА] {customerName} — недостатньо коштів! Потрібно: {amount} грн, є: {_balance} грн.\n");
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_lockObject);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЗАПУСК СИСТЕМИ БАНКОМАТІВ ===");

            BankAccount account = new BankAccount(1000);

            Console.WriteLine("Початковий баланс рахунку: 1000 грн\n");

            ThreadPool.QueueUserWorkItem(_ => account.Withdraw("Банкомат #1 (Центр)", "Олексій", 700));
            ThreadPool.QueueUserWorkItem(_ => account.Withdraw("Банкомат #2 (Вокзал)", "Марія", 500));
            ThreadPool.QueueUserWorkItem(_ => account.Withdraw("Банкомат #3 (Метро)", "Іван", 400));

            Thread.Sleep(4500);

            Console.WriteLine("=== УСІ ТРАНЗАКЦІЇ ОБРОБЛЕНО ===");
        }
    }
}