using Microsoft.Extensions.Configuration; // <-- Added this so ConfigurationBuilder works
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
        services.AddScoped<SchoolService>();

        var serviceProvider = services.BuildServiceProvider();

        var schoolService = serviceProvider.GetRequiredService<SchoolService>();

        //schoolService.RegisterNewStudent("Alice Smith", "alice@school.com");

        Console.WriteLine("Registered Students:");
        foreach (var student in schoolService.ListStudents())
        {
            Console.WriteLine($"- {student.Name} ({student.Email})");
        }
    }
}