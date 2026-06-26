using System;
using System.Collections.Generic;

public class Instructor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}