using System;
using System.Collections.Generic;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
