using LoLBoosting.Contracts.Models;

namespace LoLBoosting.Data.Repository
{
    public interface IDeletebleEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        void SoftDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}