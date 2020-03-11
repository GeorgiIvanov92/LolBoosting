using LoLBoosting.Contracts;
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
            _set.Add(newEntity);
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public T Find(object id)
        {
            // return _set.FirstOrDefault<T>(a => a.Id == id);
            throw new Exception("Mariqn will fix this");
        }

        public Task<T> FindAsync(object id)
        {
            //return _set.FirstOrDefaultAsync<T>(a => a.Id == id);
            throw new Exception("Mariqn will fix this");
        }

        public IQueryable<T> GetAll()
        {
            return _set.AsQueryable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }
    }
}
