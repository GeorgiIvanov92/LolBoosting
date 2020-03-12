using LolBoosting.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private DbContext _context;
        private DbSet<T> _set;

        public BaseRepository(LolBoostingDbContext dbContext)
        {
            _context = dbContext;
            _set = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _set.AsQueryable();
        }

        public T Find(object id)
        {
            return _set.Find(id);
        }

        public async Task<T> FindAsync(object id)
        {
            return await _set.FindAsync(id);
        }

        public void Add(T newEntity)
        {
            _set.Add(newEntity);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
