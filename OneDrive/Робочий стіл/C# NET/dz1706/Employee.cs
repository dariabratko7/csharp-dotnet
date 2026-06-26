public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}