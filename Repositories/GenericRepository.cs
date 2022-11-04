using Task1.Repositories;

namespace Task1.Repositories;

public class GenericRepository<TEntity> : IGenericRrepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async ValueTask<TEntity> AddAsync(TEntity entity, string userId)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);

        await _context.SaveChangesAsync(userId);

        return entry.Entity;
    }

    public IQueryable<TEntity> GetAll()
     => _context.Set<TEntity>();

    public async ValueTask<TEntity> Remove(TEntity entity,string userId)
    {
        var entry = _context.Set<TEntity>().Remove(entity);

        await _context.SaveChangesAsync(userId);

        return entry.Entity;
    }

    public async ValueTask<TEntity> Update(TEntity entity, string userId)
    {
        var entry = _context.Set<TEntity>().Update(entity);

        await _context.SaveChangesAsync(userId);

        return entry.Entity;
    }

     public TEntity? GetById(int id)
       => _context.Set<TEntity>().Find(id);

}