namespace UserProduct.Core.Abstractions
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        public void Update(TEntity entity);

        public Task Add(TEntity entity);

        public Task<TEntity?> FindById(string id);

        public IQueryable<TEntity> GetAll();

        public void Remove(TEntity entity);
    }
}
