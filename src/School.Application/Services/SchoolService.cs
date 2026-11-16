using School.Domain;
using School.Application.Interfaces;

namespace School.Application.Services;

public class SchoolService
{
  private readonly IStudentRepository _studentRepository;

  public SchoolService(IStudentRepository studentRepository)
  {
    _studentRepository = studentRepository;
  }

  public void RegisterNewStudent(string name, string email)
  {
    var student = new Student
    {
      Name = name,
      Email = email
    };
    _studentRepository.AddStudent(student);
  }
  public IEnumerable<Student> GetAllStudents()
  {
    return _studentRepository.GetAllStudents();
  }

  public IEnumerable<Student> ListStudents()
  {
    return _studentRepository.GetAllStudents();
  }

  public Student? GetStudentById(Guid id)
  {
    return _studentRepository.GetStudentById(id);
  }

  public void AddStudent(Student student)
  {
    _studentRepository.AddStudent(student);
  }

  public void UpdateStudent(Student student)
  {
    _studentRepository.UpdateStudent(student);
  }

  public void DeleteStudent(Guid id)
  {
    _studentRepository.DeleteStudent(id);
  }
}