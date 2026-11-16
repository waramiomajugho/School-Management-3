using School.Domain;

namespace School.Application.Interfaces;

public interface IStudentRepository
{
    IEnumerable<Student> GetAllStudents();
    Student? GetStudentById(Guid id);
    void AddStudent(Student student);
    void UpdateStudent(Student student);
    void DeleteStudent(Guid id);
}