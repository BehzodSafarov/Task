namespace Task1.Repositories;

public interface IGenericRrepository<TEntity> where TEntity : class
{
    TEntity? GetById (int id);
    IQueryable<TEntity> GetAll();
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask<TEntity> Remove(TEntity entity);
    ValueTask<TEntity> Update(TEntity entity);
    void Save(string userId);
}