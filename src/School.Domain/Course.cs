namespace School.Domain;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
}