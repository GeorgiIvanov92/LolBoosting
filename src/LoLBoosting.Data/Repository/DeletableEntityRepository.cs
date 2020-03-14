using LoLBoosting.Data.Context;
using LoLBoosting.Contracts.Models;
using System;

namespace LoLBoosting.Data.Repository
{
    public class DeletableEntityRepository<TEntity> : BaseRepository<TEntity>, IDeletebleEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        public DeletableEntityRepository(LolBoostingDbContext context) : base(context)
        {
        }
        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            Update(entity);
        }

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            Update(entity);
        }
    }
}
