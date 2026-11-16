using School.Domain;

namespace School.Application.Interfaces;

public interface ITeacherRepository
{
    IEnumerable<Teacher> GetAll();
    void Add(Teacher teacher);
}