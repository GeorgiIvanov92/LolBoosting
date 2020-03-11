using System.Linq;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Repository
{
    interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T Find(object id);

        Task<T> FindAsync(object id);

        void Add(T newEntity);

        void Update(T entity);

        void Delete(T entity);

        void Save();

        Task SaveChangesAsync();
    }
}
