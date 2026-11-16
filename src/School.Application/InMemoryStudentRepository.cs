using School.Domain; 

using School.Application.Interfaces;

namespace School.Application;

public class InMemoryStudentRepository : IStudentRepository
{
  private readonly List<Student> _students = new List<Student>();

  public IEnumerable<Student> GetAllStudents()
  {
    return _students;
  }

  public Student? GetStudentById(Guid id)
  {
    return _students.FirstOrDefault(s => s.studentid == id);
  }

  public void AddStudent(Student student)
  {
    _students.Add(student);
  }

  public void UpdateStudent(Student student)
  {
    var existingStudent = GetStudentById(student.studentid);
    if (existingStudent != null)
    {
      existingStudent.Name = student.Name;
      existingStudent.Email = student.Email;
    }
  }

  public void DeleteStudent(Guid id)
  {
    var student = GetStudentById(id);
    if (student != null)
    {
      _students.Remove(student);
    }
  }
}
