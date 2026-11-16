using School.Domain;
using School.Application.Interfaces;

namespace School.Application.Services;

public class SchoolService
{
  private readonly IStudentRepository _studentRepository;
  private readonly ITeacherRepository _teacherRepository;
  private readonly ICourseRepository _courseRepository;

  
  public SchoolService(
      IStudentRepository studentRepository, 
      ITeacherRepository teacherRepository, 
      ICourseRepository courseRepository)
  {
    _studentRepository = studentRepository;
    _teacherRepository = teacherRepository;
    _courseRepository = courseRepository;
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

  

  public void HireTeacher(string name, string department, bool isDepartmentHead)
  {
    
    if (isDepartmentHead)
    {
      var existingTeachers = _teacherRepository.GetAll();
      var departmentHasHead = existingTeachers.Any(t => 
          t.Department.Equals(department, StringComparison.OrdinalIgnoreCase) && t.IsDepartmentHead);

      if (departmentHasHead)
      {
        throw new InvalidOperationException($"Department '{department}' already has an assigned department head!");
      }
    }

    var teacher = new Teacher
    {
      Name = name,
      Department = department,
      IsDepartmentHead = isDepartmentHead
    };

    _teacherRepository.Add(teacher);
  }

  public IEnumerable<Teacher> GetAllTeachers()
  {
    return _teacherRepository.GetAll();
  }

  public void CreateCourse(string title, int maxCapacity)
  {
    
    if (maxCapacity <= 0)
    {
      throw new ArgumentException("Course max capacity must be greater than zero.");
    }

    var course = new Course
    {
      Title = title,
      MaxCapacity = maxCapacity
    };

    _courseRepository.AddCourse(course);
  }

  public IEnumerable<Course> GetAllCourses()
  {
    return _courseRepository.GetAll();
  }

}