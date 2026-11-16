

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Interfaces;
using School.Application.Services;
using School.Infrastructure;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build(); 

        var services = new ServiceCollection();

        services.Configure<StorageSettings>(configuration.GetSection("StorageSettings"));

        
        services.AddScoped<IStudentRepository, FileStudentRepository>();
        services.AddScoped<ITeacherRepository, FileTeacherRepository>();
        services.AddScoped<ICourseRepository, FileCourseRepository>();
        services.AddScoped<SchoolService>();

        var serviceProvider = services.BuildServiceProvider();

        
        var schoolService = serviceProvider.GetRequiredService<SchoolService>();

        
        schoolService.RegisterNewStudent("Alice Smith", "alice@school.com");
        
        
        try 
        {
            schoolService.HireTeacher("Dr. Alan Turing", "Computer Science", true);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Notice: {ex.Message}");
        }

        schoolService.CreateCourse("Software Architecture", 25);

       
        Console.WriteLine("\nRegistered Students:");
        foreach (var student in schoolService.ListStudents())
        {
            Console.WriteLine($"- {student.Name} ({student.Email})");
        }

        Console.WriteLine("\nHired Teachers:");
        foreach (var teacher in schoolService.GetAllTeachers())
        {
            Console.WriteLine($"- {teacher.Name} | Dept: {teacher.Department} | Head: {teacher.IsDepartmentHead}");
        }

        Console.WriteLine("\nAvailable Courses:");
        foreach (var course in schoolService.GetAllCourses())
        {
            Console.WriteLine($"- {course.Title} (Max Capacity: {course.MaxCapacity})");
        }
    }
}