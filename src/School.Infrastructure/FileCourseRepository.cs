using Microsoft.Extensions.Options;
using School.Application.Interfaces;
using School.Domain;
using System.Text.Json;

namespace School.Infrastructure;

public class FileCourseRepository : ICourseRepository
{
    private readonly string _filePath;        

    public FileCourseRepository(IOptions<StorageSettings> settings)
    {
        Directory.CreateDirectory(settings.Value.DataDirectory);
        _filePath = Path.Combine(settings.Value.DataDirectory, "courses.json");
    }

    public IEnumerable<Course> GetAll()
    {
        if (!File.Exists(_filePath)) 
        {
            return new List<Course>();
        }

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Course>>(json) ?? new List<Course>();
    }
  
    public void AddCourse(Course course)
    {
        var courses = GetAll().ToList();
        courses.Add(course);
        
        var json = JsonSerializer.Serialize(courses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}