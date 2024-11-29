using SchoolApp.Data;

namespace SchoolApp.Abstractions
{
    public interface IStudentRepository
    {
        Task<List<Course>> GetStudentCoursesAsync(int id);
        Task<Student?> GetByAm(string? am);
        Task<List<User>> GetAllUsersStudentsAsync();
    }
}
