using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private DbContext _context;
        private DbSet<T> _set;

        public BaseRepository(DbContext dbContext)
        {
            _context = dbContext;
            _set = _context.Set<T>();
        }

        public void Add(T newEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Find(object id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
