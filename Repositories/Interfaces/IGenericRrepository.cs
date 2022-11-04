namespace Task1.Repositories;

public interface IGenericRrepository<TEntity> where TEntity : class
{
    TEntity? GetById (int id);
    IQueryable<TEntity> GetAll();
    ValueTask<TEntity> AddAsync(TEntity entity, string userId);
    ValueTask<TEntity> Remove(TEntity entity, string userId);
    ValueTask<TEntity> Update(TEntity entity, string userId);
}