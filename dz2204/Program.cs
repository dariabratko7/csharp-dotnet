using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Створити клас «TeachersWebinar», з властивостями: ім'я, вік та запрошення.  Створіть у класі статичне поле запрошення. На вебінар можуть потрапити
//    учасники лише старше 25 років та із запрошенням – “VeInar2021”. 
//    В разі невідповідності, генерувати помилку через свій клас винятків. При виведенні інформації про помилку – виводити, що не так і саме значення.

//public class WebinarException : Exception
//{
//    public WebinarException(string message) : base(message) { }
//}

//public class TeachersWebinar
//{
//    public string Name { get; set; }
//    public int Age { get; set; }

//    public static string RequiredInvite = "VeInar2021";

//    private string invite;

//    public string Invite
//    {
//        get { return invite; }
//        set
//        {
//            invite = value;
//        }
//    }

//    public TeachersWebinar(string name, int age, string invite)
//    {
//        Name = name;
//        Age = age;
//        Invite = invite;

//        Validate();
//    }

//    private void Validate()
//    {
//        if (Age <= 25)
//        {
//            throw new WebinarException($"Помилка: вік менше або дорівнює 25! Значення: {Age}");
//        }

//        if (Invite != RequiredInvite)
//        {
//            throw new WebinarException($"Помилка: неправильне запрошення! Значення: {Invite}");
//        }
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            TeachersWebinar t1 = new TeachersWebinar("Іван", 30, "VeInar2021");
//            Console.WriteLine("Учасник допущений!");
//        }
//        catch (WebinarException ex)
//        {
//            Console.WriteLine(ex.Message);
//        }

//        try
//        {
//            TeachersWebinar t2 = new TeachersWebinar("Петро", 22, "VeInar2021");
//        }
//        catch (WebinarException ex)
//        {
//            Console.WriteLine(ex.Message);
//        }

//        try
//        {
//            TeachersWebinar t3 = new TeachersWebinar("Оля", 28, "WrongCode");
//        }
//        catch (WebinarException ex)
//        {
//            Console.WriteLine(ex.Message);
//        }
//    }
//}

//У газетному виданні для публікації статті є певні норми. Ваше завдання, пропускати статті, які містять
//    щонайменше 30 пропозицій та 1000 слів. Розробте клас із властивостями: назва, текст, автор. У разі невідповідності тексту, не приймайте статтю, 
//    повертайте помилку, з інформацією чого не вистачило (пропозицій чи слів). Для вилову помилок використовувати конструкцію try/catch.
public class ArticleException : Exception
{
    public ArticleException(string message) : base(message) { }
}

public class Article
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }

    public Article(string title, string text, string author)
    {
        Title = title;
        Text = text;
        Author = author;

        Validate();
    }

    private void Validate()
    {
        int sentenceCount = CountSentences(Text);
        int wordCount = CountWords(Text);

        if (sentenceCount < 30 && wordCount < 1000)
        {
            throw new ArticleException(
                $"Недостатньо речень ({sentenceCount}) і слів ({wordCount})"
            );
        }
        else if (sentenceCount < 30)
        {
            throw new ArticleException(
                $"Недостатньо речень! Є: {sentenceCount}"
            );
        }
        else if (wordCount < 1000)
        {
            throw new ArticleException(
                $"Недостатньо слів! Є: {wordCount}"
            );
        }
    }

    private int CountSentences(string text)
    {
        char[] delimiters = { '.', '!', '?' };
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    private int CountWords(string text)
    {
        return text.Split(new char[] { ' ', '\n', '\t' },
            StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            string text = "Тут має бути дуже довгий текст..."; // тест

            Article article = new Article(
                "Моя стаття",
                text,
                "Автор"
            );

            Console.WriteLine("Стаття прийнята!");
        }
        catch (ArticleException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}

