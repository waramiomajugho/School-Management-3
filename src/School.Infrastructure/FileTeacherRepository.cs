using Microsoft.Extensions.Options;
using School.Application.Interfaces;
using School.Domain;
using System.Text.Json;

namespace School.Infrastructure;

public class FileTeacherRepository : ITeacherRepository
{
    private readonly string _filePath;

    public FileTeacherRepository(IOptions<StorageSettings> settings)
    {
      
        Directory.CreateDirectory(settings.Value.DataDirectory);
        _filePath = Path.Combine(settings.Value.DataDirectory, "teachers.json");
    }

    public IEnumerable<Teacher> GetAll()
    {
        if (!File.Exists(_filePath)) 
        {
            return new List<Teacher>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Teacher>>(json) ?? new List<Teacher>();
    }

    public void Add(Teacher teacher)
    {
        var teachers = GetAll().ToList();
        teachers.Add(teacher);
        
        var json = JsonSerializer.Serialize(teachers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}