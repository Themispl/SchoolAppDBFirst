
using Microsoft.EntityFrameworkCore;
using SchoolApp.Abstractions;
using SchoolApp.Data;

namespace SchoolApp.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly Student6DbContext context;
        protected readonly DbSet<T>dbset;

        public BaseRepository(Student6DbContext student6DbContext)
        {
            context = student6DbContext;
            dbset = student6DbContext.Set<T>(); // dynamically retrieves Dbset
        }

        public virtual async Task AddAsync(T entity) => await dbset.AddAsync(entity);
        
        public virtual async Task AddRangeAsync(IEnumerable<T> entities) => await dbset.AddRangeAsync(entities);

        public virtual Task UpdateAsync(T entity)
        {
           dbset.Attach(entity);
           context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
           T? existiingEntity = await GetAsync(id);
            if (existiingEntity is null ) return false;
            dbset.Remove(existiingEntity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await dbset.ToListAsync();
        
        public virtual async Task<T?> GetAsync(int id) => await dbset.FindAsync(id);

        public virtual async Task<int> GetCountAsync() => await dbset.CountAsync();
        
    }
}
