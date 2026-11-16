namespace School.Domain;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public bool IsDepartmentHead { get; set; }
}