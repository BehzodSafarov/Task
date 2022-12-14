using Task1.Repositories;

namespace Task1.Repositories;

public class GenericRepository<TEntity> : IGenericRrepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async ValueTask<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);

        return entry.Entity;
    }

    public IQueryable<TEntity> GetAll()
     => _context.Set<TEntity>();

    public async ValueTask<TEntity> Remove(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Remove(entity);


        return entry.Entity;
    }

    public async ValueTask<TEntity> Update(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Update(entity);

        return entry.Entity;
    }

     public TEntity? GetById(int id)
       => _context.Set<TEntity>().Find(id);
    
    public async void Save(string userId)
    {
       await _context.SaveChangesAsync(userId);
    }

}