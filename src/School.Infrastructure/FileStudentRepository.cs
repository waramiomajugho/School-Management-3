using System.Text.Json;
using Microsoft.Extensions.Options;
using School.Application.Interfaces;
using School.Domain;

namespace School.Infrastructure;

public class FileStudentRepository : IStudentRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public FileStudentRepository(IOptions<StorageSettings> options)
    {
        var directory = options.Value.DataDirectory;

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        _filePath = Path.Combine(directory, "students.json");
    }

    public IEnumerable<Student> GetAllStudents()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Student>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Student>>(json, _jsonOptions) ?? new List<Student>();
    }

    public Student? GetStudentById(Guid id) => GetAllStudents().FirstOrDefault(student => student.studentid == id);

    public void AddStudent(Student student)
    {
        var students = GetAllStudents().ToList();
        students.Add(student);
        SaveToFile(students);
    }

    public void UpdateStudent(Student student)
    {
        var students = GetAllStudents().ToList();
        var index = students.FindIndex(s => s.studentid == student.studentid);
        if (index != -1)
        {
            students[index] = student;
            SaveToFile(students);
        }
    }

    public void DeleteStudent(Guid id)
    {
        var students = GetAllStudents().ToList();
        students.RemoveAll(s => s.studentid == id);
        SaveToFile(students);
    }

    private void SaveToFile(List<Student> students)
    {
        var json = JsonSerializer.Serialize(students, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}