using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//Вам надано рядок тексту, що містить адреси електронної пошти, розділені комами. Напишіть програму на C#, яка використовує регулярні вирази для
//отримання кожної адреси електронної пошти з рядка, а потім перевіряє кожну адресу електронної пошти, щоб переконатися, що вона має правильний формат.
//Наприклад, якщо дано рядок "john.doe@example.com, jane.doe@example.com, invalid_email_address, bob@example.com", програма має вивести:
//john.doe @example.com
//jane.doe @example.com
//bob@example.com
//Програма повинна використовувати регулярні вирази для отримання кожної адреси електронної пошти з рядка, а потім перевірити кожну адресу
//електронної пошти за допомогою шаблону регулярного виразу, який відповідає синтаксису дійсної адреси електронної пошти.


class Program
{
    static void Main()
    {
        string input = "john.doe@example.com, jane.doe@example.com, invalid_email_address, bob@example.com";

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        string[] emails = input.Split(',');

        foreach (string email in emails)
        {
            string trimmedEmail = email.Trim();

            if (Regex.IsMatch(trimmedEmail, pattern))
            {
                Console.WriteLine(trimmedEmail);
            }
        }
    }
}

//Вам необхідно перевірити, що заданий рядок введення містить правильну URL-адресу. URL повинен починатися
//    з "http://" або "https://"" і містити хоча б одну точку після протоколу. URL також може містити необов'язкові параметри шляху та запиту.

class Program
{
    static void Main()
    {
        Console.Write("Введіть URL: ");
        string url = Console.ReadLine();

        string pattern = @"^https?:\/\/([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/.*)?$";

        if (Regex.IsMatch(url, pattern))
        {
            Console.WriteLine("URL правильний");
        }
        else
        {
            Console.WriteLine("URL неправильний");
        }
    }
}