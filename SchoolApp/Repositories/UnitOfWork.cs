using SchoolApp.Abstractions;
using SchoolApp.Data;

namespace SchoolApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Student6DbContext _context;

        public UnitOfWork(Student6DbContext context)
        {
            _context = context;
        }

        public TeacherRepository TeacherRepository => new(_context);

        public StudentRepository StudentRepository => new(_context);

        public CourseRepository CourseRepository => new(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
