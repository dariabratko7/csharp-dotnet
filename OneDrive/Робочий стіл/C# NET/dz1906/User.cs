using System;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime BirthDate { get; set; }
}