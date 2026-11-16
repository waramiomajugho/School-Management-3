using School.Domain;

namespace School.Application;

public interface ITeacherRepository
{
    IEnumerable<Teacher> GetAll();
    void Add(Teacher teacher);
}