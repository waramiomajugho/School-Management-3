using School.Domain;

namespace School.Application.Interfaces;

public interface ICourseRepository
{
    IEnumerable<Course> GetAll();
    void AddCourse(Course course);
}