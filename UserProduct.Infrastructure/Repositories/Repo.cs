using Microsoft.EntityFrameworkCore;
using UserProduct.Core.Abstractions;
using UserProduct.Data.Data;
using UserProduct.Domain.Entities;

namespace UserProduct.Infrastructure.Repositories
{
    public class Repo<TEntity> : IRepo<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _entitySet;

        public Repo(AppDbContext context)
        {
            _entitySet = context.Set<TEntity>();
        }


        public async Task<TEntity?> FindById(string id)
        {
            return await _entitySet.FindAsync(id) ?? null;
        }


        public void Update(TEntity entity)
        {
            _entitySet.Update(entity);
        }


        public IQueryable<TEntity> GetAll()
        {
            return _entitySet;
        }

    }
}
